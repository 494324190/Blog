//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace BM.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_Article
    {
        public tb_Article()
        {
            this.tb_Comment = new HashSet<tb_Comment>();
        }
    
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public System.DateTime Date { get; set; }
        public string ClassificationId { get; set; }
        public string Abstract { get; set; }
        public Nullable<int> LikeNum { get; set; }
    
        public virtual tb_ArticleClassification tb_ArticleClassification { get; set; }
        public virtual ICollection<tb_Comment> tb_Comment { get; set; }
    }
}
