namespace wpf页面设计WebAPI.DTOs
{
    /// <summary>
    /// 数据传输对象-->接收webapi数据和传输给业务数据
    ///  账号DTO(用来注册信息)
    /// </summary>
    public class AccountInfoDTO
    {
        public string Name { get; set; }
        public string Account { get; set; }
        public string Pwd { get; set; }
    }
}
