using Microsoft.EntityFrameworkCore;


namespace wpf页面设计WebAPI.DataModel
{
    // 数据库上下文
    public class DailyContext:DbContext
    {
        /// <summary>
        ///  构造函数
        /// </summary>
        /// <param name="options"></param>
        public DailyContext(DbContextOptions<DailyContext> options) : base(options) 
        { 
        
        }


        /* 完成数据库上下文属性后进行数据迁移(创建表)
         * add-migration (name)
         * update-database
         */

        /// <summary>
        ///  账号
        /// </summary>
        /// virtua
        /// 延时加载
        public virtual DbSet<AccountInfo> AccountInfos { get; set; }


        /// <summary>
        /// 代办事项
        /// </summary>
        public virtual DbSet<WaitInfo> WaitInfos { get; set; }

        /// <summary>
        /// 备忘录
        /// </summary>
        public virtual DbSet<MeMoInfo> memoInfos { get; set; }
    }
}
