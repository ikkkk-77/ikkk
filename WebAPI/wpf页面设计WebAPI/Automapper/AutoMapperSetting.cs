using AutoMapper;
using Wpf页面设计_Prism_WebAPI_.DTOs;
using wpf页面设计WebAPI.DataModel;
using wpf页面设计WebAPI.DTOs;

namespace wpf页面设计WebAPI.Automapper
{
    /// <summary>
    /// Model转换
    /// </summary>
    public class AutoMapperSetting:Profile
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AutoMapperSetting()
        {
            //DTO转换成数据模型Info
            CreateMap<AccountInfoDTO, AccountInfo>().ReverseMap();
            CreateMap<AddWaitInfoDTO, WaitInfo>().ReverseMap();
            CreateMap<MeMoInfoDTO, MeMoInfo>().ReverseMap();
        }
    }
}
