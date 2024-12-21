using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wpf页面设计WebAPI.APIRespongse;
using wpf页面设计WebAPI.DataModel;
using wpf页面设计WebAPI.DTOs;

namespace wpf页面设计WebAPI.Controllers
{
    /// <summary>
    /// 用户注册控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class account_Controller : ControllerBase
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        private readonly DailyContext db;

        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper mapper;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_db"></param>
        /// <param name="_mapper"></param>
        public account_Controller(DailyContext _db,IMapper _mapper)
        {
            // 赋值
            db = _db;
            mapper = _mapper;
        }


        /// <summary>
        ///  用户注册方法
        /// </summary>
        /// <param name="accountInfoDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Reg(AccountInfoDTO accountInfoDTO)
        {
            // 响应的数据
            ApiResponse response = new ApiResponse();

            // 业务
            try
            {
                // 1、账号已经存在(没有考虑高并发)
                // (将 LINQ 表达式转换为 SQL 查询)
                var dbAccount = db.AccountInfos.Where(t => t.Account == accountInfoDTO.Account).FirstOrDefault();

                if (dbAccount != null)
                {
                    // "账号已经存在"
                    response.ResultCode = -1;
                    response.Msg = "注册失败,账号存在";

                    return Ok(response);
                }


                // 2、账号不存在,数据库添加账号 (使用AutoMapper组件使DTO转换成数据模型)
                //AccountInfo accountInfo = new AccountInfo() { Account = accountInfoDTO.Account,Name = accountInfoDTO.Name,Pwd = accountInfoDTO.Pwd };
                
                AccountInfo accountInfo = mapper.Map<AccountInfo>(accountInfoDTO); // 转换 DTO->Info

                db.AccountInfos.Add(accountInfo); // 数据库添加
                int result = db.SaveChanges();  // 保存,受影响的行数

                if (result == 1)
                {
                    //"账号注册成功"
                    response.ResultCode = 1;
                    response.Msg = "注册成功";
                }
                else
                {
                    // "账号注册失败"
                    response.ResultCode = -99;
                    response.Msg = "注册失败";
                }
            }
            catch
            {
                // "账号注册失败"
                response.ResultCode = -999;
                response.Msg = "服务器异常,注册失败";
            }

            return Ok(response);
        }



        /// <summary>
        /// 用户登录方法
        /// </summary>
        /// <param name="loginInfoDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(LoginInfoDTO loginInfoDTO) 
        {
            // 创建API响应对象
            ApiResponse response = new ApiResponse();

            // 业务
            try 
            {
                // 判断账号密码是否一致 (有则返回AccountInfo对象)
                var result = db.AccountInfos.Where
                (t => loginInfoDTO.Account == t.Account && loginInfoDTO.Pwd == t.Pwd).FirstOrDefault();

                // 判断
                if (result == null)
                {
                    // 登录失败
                    response.ResultCode = -1;
                    response.Msg = "登录失败";
                    return Ok(response);
                }
                else
                {
                    // 登录成功
                    response.ResultCode = 1;  // 编码
                    response.Msg = "登录成功";  // 信息
                    response.ResultData = result; // 数据
                }
            }
            catch { 
                response.ResultCode = -999; // 登录失败
                response.Msg = "登录失败";
            }

                return Ok(response);
        }
    }
}
