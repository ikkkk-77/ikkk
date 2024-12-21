using System.ComponentModel.DataAnnotations;

namespace wpf页面设计WebAPI.DTOs
{
    public class MeMoInfoDTO
    {
        // ID
        [Key]
        public int WaitId { get; set; }

        // 标题
        public string Title { get; set; }

        // 内容
        public string Content { get; set; }

        // 状态 0-代办 1-已完成
        public int Status { get; set; }

        /// <summary>
        /// 备忘录总数
        /// </summary>
        public int TotalCount { get; set; }
    }
}
