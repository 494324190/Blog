using BM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BM.DAL;

namespace BM.BLL
{
    public class CommentBLL
    {
        CommentDAL commentDal = new CommentDAL();
        public List<tb_Comment> GetTopComment(int top,Func<tb_Comment,object> orderby)
        {
            return commentDal.GetTopComment(top, orderby);
        }
    }
}
