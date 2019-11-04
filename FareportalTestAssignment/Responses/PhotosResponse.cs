using System.Collections.Generic;

namespace FareportalTestAssignment.Responses
{
    public class PhotosResponse
    {
        public List<Photo> PhotosList { get; set; }
    }

    public class Photo
    {
        public int albumId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string thumbnailUrl { get; set; }
    }
}
