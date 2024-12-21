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
    /// 待办事项统计控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WaitController : ControllerBase
    {
        // 数据库上下文对象对象
        private readonly DailyContext db;
        // AutoMapper
        private readonly IMapper mapper;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_db"></param>
        public WaitController(DailyContext _db, IMapper _mapper)
        {
            // 赋值
            this.db = _db;
            this.mapper = _mapper;
        }


       

        /// <summary>
        /// 返回首页面板数据(待办事项数据)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetWaitData()
        {
            // 创建响应数据对象
            ApiResponse apiResponse = new ApiResponse();

            // 业务
            try
            {
                var list = db.WaitInfos.ToList();  // 所有记录
                var finishList = list.Where(t => t.Status == 1).ToList();  // 已完成记录

                WaitInfoDTO waitInfoDTO = new WaitInfoDTO(); // 创建WaitInfoDTO对象
                waitInfoDTO.TotalCount = list.Count;  // 赋值
                waitInfoDTO.FinishCount = finishList.Count;

                

                // 响应结果赋值
                apiResponse.ResultCode = 1;
                apiResponse.ResultData = waitInfoDTO;
                apiResponse.Msg = "待办事项统计成功";
            }
            catch
            {
                apiResponse.ResultCode = -99;
                apiResponse.Msg = "统计待办事项失败";
            }
            // 返回
            return Ok(apiResponse);
        }



        /// <summary>
        /// 返回具体的待办事项数据(首页列表显示)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetWaitDataDetail()
        {
            // 创建响应数据对象
            ApiResponse apiResponse = new ApiResponse();

            // 业务
            try
            {
                // 获取数据库中的WaitInfo对象并赋值给WaitInfoDTO(状态为0)
                var waitInfoDTO = from A in db.WaitInfos
                                  where A.Status == 0
                                  select new WaitInfoDTO
                                  {
                                      WaitId = A.WaitId,
                                      Title = A.Title,
                                      Content = A.Content,
                                      Status = A.Status,
                                  };

                // 判断
                if (waitInfoDTO != null)
                {
                    // 响应结果赋值
                    apiResponse.ResultCode = 1;
                    apiResponse.ResultData = waitInfoDTO;
                    apiResponse.Msg = "待办事项统计成功";
                }
            }
            catch
            {
                apiResponse.ResultCode = 99;
                apiResponse.Msg = "找不到待办事项";
            }

            return Ok(apiResponse);
        }



        /// <summary>
        /// 返回具体的待办事项数据(待办事项页显示) (暂时不用)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetWaitingDataDetail()
        {
            // 创建响应数据对象
            ApiResponse apiResponse = new ApiResponse();

            // 业务
            try
            {
                var waitInfoDTO = db.WaitInfos.ToList();

                // 判断
                if (waitInfoDTO != null)
                {
                    // 响应结果赋值
                    apiResponse.ResultCode = 1;
                    apiResponse.ResultData = waitInfoDTO;
                    apiResponse.Msg = "待办事项统计成功";
                }
            }
            catch
            {
                apiResponse.ResultCode = 99;
                apiResponse.Msg = "找不到待办事项";
            }

            return Ok(apiResponse);
        }



        /// <summary>
        /// 加入待办事项数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddWaitData(AddWaitInfoDTO addWaitInfoDTO)
        {
            // 创建API响应对象
            ApiResponse apiResponse = new ApiResponse();

            // 业务
            try
            {
                // 查找是否已存在待办事项
                var result = db.WaitInfos.Where(t => t.Title == addWaitInfoDTO.Title).FirstOrDefault();

                // 判断
                if (result != null)
                {
                    apiResponse.ResultCode = -99;
                    apiResponse.Msg = "该代办事项已经存在,请重新输入";
                }
                else
                {
                    // WaitInfoDTO转换成WaitInfo对象
                    WaitInfo waitInfo = mapper.Map<WaitInfo>(addWaitInfoDTO);

                    // 添加表中
                    db.WaitInfos.Add(waitInfo);
                    // 保存
                    int result2 = db.SaveChanges();

                    if (result2 == 1)
                    {
                        apiResponse.ResultCode = 1;
                        apiResponse.Msg = "添加代办事项成功";
                    }
                }
            }
            catch {
                apiResponse.ResultCode = -999;
                apiResponse.Msg = "服务器异常";
            }

            return Ok(apiResponse);
        }



        /// <summary>
        /// 修改待办事项数据
        /// </summary>
        /// <param name="WaitInfoDTO"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult EditWaitData(WaitInfoDTO waitInfoDTO)
        {
            ApiResponse response = new ApiResponse();

            // 业务
            try
            {
                // 寻找对应WaitInfoDTO的ID的值
                var dbInfo = db.WaitInfos.Find(waitInfoDTO.WaitId);

                if (dbInfo != null) {
                    // 编辑修改
                    dbInfo.Status = waitInfoDTO.Status;
                    dbInfo.Title = waitInfoDTO.Title;
                    dbInfo.Content = waitInfoDTO.Content;

                    // 保存
                    int result = db.SaveChanges();

                    if (result == 1)
                    {
                        response.ResultCode = 1;
                        response.Msg = "修改成功";
                    }
                    else
                    {
                        response.ResultCode = -1;
                        response.Msg = "修改失败";
                    }
                }
                else
                {
                    response.ResultCode = -99;
                    response.Msg = "请检查Id值是否存在";
                }
            }
            catch 
            {
                response.ResultCode = -999;
                response.Msg = "服务器异常";
            }

            return Ok(response);
        }


        /// <summary>
        ///  修改待办事项的状态
        /// </summary>
        /// <param name="waitInfoDTO"></param>
        [HttpPut]
        public IActionResult UpdateStatus(WaitInfoDTO waitInfoDTO)
        {
            // 创建API响应对象
            ApiResponse apiResponse = new ApiResponse();

            // 业务
            try
            {
                // 通过Id查找对应的待办事项 (Find的性能比where高)
                var dbInfo = db.WaitInfos.Find(waitInfoDTO.WaitId);

                // 判断
                if (dbInfo != null)
                {
                    // 状态改变
                    dbInfo.Status = waitInfoDTO.Status;
                    // 保存
                    int result =  db.SaveChanges();

                    if (result == 1)
                    {
                        apiResponse.ResultCode = 1;
                        apiResponse.Msg = (waitInfoDTO.Status == 0 ? "状态成功设置为代办" : "状态成功设置为已完成");
                    }
                    else
                    {
                        apiResponse.ResultCode = -1;
                        apiResponse.Msg = "更改失败,请重新尝试";
                    }
                }
                else
                {
                    apiResponse.ResultCode = -99;
                    apiResponse.Msg = "检查待办事项Id是否存在,请重新尝试";
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
        /// 搜索待办事项数据
        /// </summary>
        /// <param name="title"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SearchWait(string? title,int? status)
        {
            // 搜索标题title为空时表示只查找代办或已完成事项, 不为空时需要判断事项标题是否包含搜索内容
            // 搜索状态status为空表示查找全部

            ApiResponse response = new ApiResponse();

            // 业务
            try
            {
                // 查找全部(再进行单独判断)
                var waitinfoDTO = from A in db.WaitInfos
                                  select new WaitInfoDTO
                                  {
                                      WaitId = A.WaitId,
                                      Title = A.Title,
                                      Status = A.Status,
                                      Content = A.Content,
                                  };

                if (waitinfoDTO != null)
                {
                    // 搜索标题不为空时
                    if (!string.IsNullOrEmpty(title))
                    {
                        // 是否包含搜索标题
                        waitinfoDTO = waitinfoDTO.Where(t => (t.Title.Contains(title)));
                    }

                    // 搜索状态不为空时
                    if (status != null)
                    {
                        // 是否符合事项状态
                        waitinfoDTO = waitinfoDTO.Where(t=>(t.Status == status));
                    }

                    response.ResultCode = 1;
                    response.Msg = "查询成功";
                    response.ResultData = waitinfoDTO;
                }
                else
                {
                    response.ResultCode = -99;
                    response.Msg = "查询失败";
                }
            }
            catch
            {
                response.ResultCode = -999;
                response.Msg = "服务器繁忙";
            }

            return Ok(response);
        }


        /// <summary>
        /// 删除待办事项数据
        /// </summary>
        /// <param name="WaitId"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteWaitData(int WaitId)
        {
            ApiResponse apiResponse = new ApiResponse();

            // 业务
            try
            {
                // 寻找待删除待办事项
                var result = db.WaitInfos.Find(WaitId);

                if (result != null)
                {
                    // 删除待办事项
                    db.WaitInfos.Remove(result);
                    // 保存
                    int res =  db.SaveChanges();

                    if (res == 1)
                    {
                        apiResponse.ResultCode = 1;
                        apiResponse.Msg = "删除成功";
                    }
                }
                else
                {
                    apiResponse.Msg = "找不到该待办事项,删除失败";
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
