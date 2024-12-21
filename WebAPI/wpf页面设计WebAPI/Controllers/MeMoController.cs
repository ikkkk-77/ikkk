using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wpf页面设计_Prism_WebAPI_.DTOs;
using wpf页面设计WebAPI.APIRespongse;
using wpf页面设计WebAPI.DataModel;
using wpf页面设计WebAPI.DTOs;

namespace wpf页面设计WebAPI.Controllers
{
    /// <summary>
    /// 备忘录统计数据控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MeMoController : ControllerBase
    {
        // 数据库上下文对象
        private readonly DailyContext db;
        // AutoMapper
        private readonly IMapper mapper;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_db"></param>
        public MeMoController(DailyContext _db, IMapper _mapper)
        {
            db = _db;
            this.mapper = _mapper;
        }


        /// <summary>
        /// 获取备忘录数据方法
        /// </summary>
        [HttpGet]
        public IActionResult GetMeMoData()
        {
            // 创建响应对象
            ApiResponse response = new ApiResponse();

            // 业务
            try
            {
                // 获取列表总数
                var resule = db.memoInfos.ToList();

                // 赋值
                MeMoInfoDTO meMoInfoDTO = new MeMoInfoDTO();
                meMoInfoDTO.TotalCount = resule.Count;

                response.Msg = "获取数据成功";
                response.ResultCode = 1;
                response.ResultData = meMoInfoDTO;
            }
            catch { response.ResultCode = -99; response.Msg = "获取数据失败"; }

            return Ok(response);
        }


        /// <summary>
        ///  获取备忘录具体数据方法(首页显示)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetMeMoDataDetail()
        {
            // 创建响应数据对象
            ApiResponse apiResponse = new ApiResponse();

            // 业务
            try
            {
                // 获取数据库中的WaitInfo对象并赋值给WaitInfoDTO(状态为0)
                var MemoInfoDTO = from A in db.memoInfos
                                  where A.Status == 0
                                  select new MeMoInfoDTO
                                  {
                                      WaitId = A.WaitId,
                                      Title = A.Title,
                                      Content = A.Content,
                                      Status = A.Status,
                                  };

                // 判断
                if (MemoInfoDTO != null)
                {
                    // 响应结果赋值
                    apiResponse.ResultCode = 1;
                    apiResponse.ResultData = MemoInfoDTO;
                    apiResponse.Msg = "备忘录统计成功";
                }
            }
            catch
            {
                apiResponse.ResultCode = 99;
                apiResponse.Msg = "找不到备忘录";
            }

            return Ok(apiResponse);
        }


        /// <summary>
        ///  获取备忘录具体数据方法(备忘录页显示) (暂时不用)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetMeMoingDataDetail()
        {
            // 创建响应数据对象
            ApiResponse apiResponse = new ApiResponse();

            // 业务
            try
            {
                // 获取列表总数
                var MemoInfoDTO = db.memoInfos.ToList();

                // 判断
                if (MemoInfoDTO != null)
                {
                    // 响应结果赋值
                    apiResponse.ResultCode = 1;
                    apiResponse.ResultData = MemoInfoDTO;
                    apiResponse.Msg = "备忘录统计成功";
                }
            }
            catch
            {
                apiResponse.ResultCode = 99;
                apiResponse.Msg = "找不到备忘录";
            }

            return Ok(apiResponse);
        }


        /// <summary>
        /// 加入备忘录数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddMeMoData(MeMoInfoDTO addmeMoInfoDTO)
        {
            // 创建API响应对象
            ApiResponse apiResponse = new ApiResponse();

            // 业务
            try
            {
                // 查找是否已存在待办事项
                var result = db.WaitInfos.Where(t => t.Title == addmeMoInfoDTO.Title).FirstOrDefault();

                // 判断
                if (result != null)
                {
                    apiResponse.ResultCode = -99;
                    apiResponse.Msg = "该备忘录已经存在,请重新输入";
                }
                else
                {
                    // WaitInfoDTO转换成WaitInfo对象
                    MeMoInfo meMoInfo = mapper.Map<MeMoInfo>(addmeMoInfoDTO);

                    // 添加表中
                    db.memoInfos.Add(meMoInfo);
                    // 保存
                    int result2 = db.SaveChanges();

                    if (result2 == 1)
                    {
                        apiResponse.ResultCode = 1;
                        apiResponse.Msg = "添加备忘录成功";
                    }
                }
            }
            catch
            {
                apiResponse.ResultCode = -999;
                apiResponse.Msg = "服务器异常";
            }

            return Ok(apiResponse);
        }

        /// <summary>
        /// 更改备忘录数据
        /// </summary>
        /// <param name="meMoInfoDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditMeMoData(MeMoInfoDTO meMoInfoDTO)
        {
            ApiResponse apiResponse = new ApiResponse();

            // 业务
            try
            {
                // 查找是否存在该备忘录
                var result = db.memoInfos.Find(meMoInfoDTO.WaitId);

                if (result != null)
                {
                    // 更改
                    result.Title = meMoInfoDTO.Title;
                    result.Content = meMoInfoDTO.Content;
                    result.Status = meMoInfoDTO.Status;

                    var result2 = db.SaveChanges();

                    if (result2 == 1)
                    {
                        apiResponse.ResultCode = 1;
                        apiResponse.Msg = "修改成功";
                    }
                }
                else
                {
                    apiResponse.ResultCode = -99;
                    apiResponse.Msg = "找不到该备忘录,请重试";
                }
            }
            catch
            {
                apiResponse.ResultCode = -999;
                apiResponse.Msg = "服务器繁忙";
            }

            return Ok(apiResponse);
        }

        /// <summary>
        /// 更新备忘录数据状态
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateMeMoStatus(MeMoInfoDTO meMoInfoDTO)
        {
            ApiResponse respongse = new ApiResponse();

            // 业务
            var dbInfo = db.memoInfos.Find(meMoInfoDTO.WaitId);

            try
            {
                if (dbInfo != null)
                {
                    dbInfo.Status = meMoInfoDTO.Status;

                    int result = db.SaveChanges();


                    if (result == 1)
                    {
                        respongse.ResultCode = 1;
                        respongse.Msg = (meMoInfoDTO.Status == 0 ? "状态成功设置为备忘" : "状态成功设置为已完成");
                    }
                    else
                    {
                        respongse.ResultCode = -1;
                        respongse.Msg = "更改失败,请重新尝试";
                    }
                }
                else
                {
                    respongse.ResultCode = -99;
                    respongse.Msg = "检查备忘录Id是否存在,请重新尝试";
                }
            }
            catch
            {
                respongse.ResultCode = -999;
                respongse.Msg = "服务器异常";
            }

            return Ok(respongse);
        }

        /// <summary>
        /// 搜索备忘录数据
        /// </summary>
        /// <param name="title"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SearchMeMo(string? title,int? status)
        {
            ApiResponse apiResponse = new ApiResponse();

            // 业务
            try 
            {
                // 查找全部,再单独判断
                var MemoInfoDTO = from A in db.memoInfos
                                  select new MeMoInfoDTO
                                  {
                                      WaitId = A.WaitId,
                                      Status = A.Status,
                                      Title = A.Title,
                                      Content = A.Content,
                                  };

                // 判断
                if (MemoInfoDTO != null)
                {
                    if (!string.IsNullOrEmpty(title))
                    {
                        MemoInfoDTO = MemoInfoDTO.Where(t => (t.Title.Contains(title)));
                    }
                    if (status != null)
                    {
                        MemoInfoDTO = MemoInfoDTO.Where(t => (t.Status == status));
                    }

                    apiResponse.ResultCode = 1;
                    apiResponse.ResultData = MemoInfoDTO;
                    apiResponse.Msg = "搜索成功";
                }
                else
                {
                    apiResponse.ResultCode = -1;
                    apiResponse.Msg = "搜索不到该备忘录";
                }
            }
            catch 
            {
                apiResponse.ResultCode = -99;
                apiResponse.Msg = "服务器繁忙";
            }

            return Ok(apiResponse);
        }

        /// <summary>
        /// 删除备忘录数据
        /// </summary>
        /// <param name="MeMoId"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteMeMoData(int MeMoId)
        {
            ApiResponse apiResponse = new ApiResponse();

            // 业务
            try
            {
                // 寻找待删除待办事项
                var result = db.memoInfos.Find(MeMoId);

                if (result != null)
                {
                    // 删除待办事项
                    db.memoInfos.Remove(result);
                    // 保存
                    int res = db.SaveChanges();

                    if (res == 1)
                    {
                        apiResponse.ResultCode = 1;
                        apiResponse.Msg = "删除成功";
                    }
                }
                else
                {
                    apiResponse.Msg = "找不到备忘录,删除失败";
                    apiResponse.ResultCode = -99;
                }
            }
            catch
            {
                apiResponse.Msg = "服务器繁忙";
                apiResponse.ResultCode = 999;
            }
            return Ok(apiResponse);
        }
    }
}
