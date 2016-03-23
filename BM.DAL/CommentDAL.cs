using BM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BM.DAL
{
    public class CommentDAL:BaseRepository<tb_Comment>
    {
        public List<Models.tb_Comment> GetTopComment(int top, Func<Models.tb_Comment, object> orderby)
        {
            try
            {
                return Getlist(p => p.Id != "", orderby).Take<tb_Comment>(top).ToList<tb_Comment>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
