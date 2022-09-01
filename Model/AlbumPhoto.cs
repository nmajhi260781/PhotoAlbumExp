using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoAlbumExp.Model
{
    //View model class for all photos and album
    public class AlbumPhoto
    {
        public int albumId { get; set; }
        public int albumUserId { get; set; }
        public string albumTitle { get; set; }
        public int photoId { get; set; }
        public string photoTitle { get; set; }
        public string photoUrl { get; set; }
        public string photoThumbnailUrl { get; set; }
    }
}
