using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoAlbumExp.Model
{
    /// <summary>
    /// Album model class
    /// </summary>
    public class Album
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string title { get; set; }
    }
}
