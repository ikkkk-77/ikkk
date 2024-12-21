namespace wpf页面设计WebAPI.DTOs
{
    /// <summary>
    /// 添加待办事项
    /// </summary>
    public class AddWaitInfoDTO
    {

        // 标题
        public string Title { get; set; }

        // 内容
        public string Content { get; set; }

        // 状态 0-代办 1-已完成
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
