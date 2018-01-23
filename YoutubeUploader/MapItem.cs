using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeUploader
{
    public class MapItem
    {
        public string MapFilePath { get; set; }
        public string Name { get; set; }
     
        public MapItem()
        {
            R = new List<FileItem>();
            PF = new List<FileItem>();
            RVO = new List<FileItem>();
            PR = new List<FileItem>();
            Rplus = new List<FileItem>();
            PFplus = new List<FileItem>();

        }

        public bool ExistsR
        {
            get
            {
                return (R.Count > 0) ? true : false;
            }
        }
        public List<FileItem> R { get; set; }

        public bool ExistsPF
        {
            get
            {
                return (PF.Count > 0) ? true : false;
            }
        }
        public List<FileItem> PF { get; set; }

        public bool ExistsRVO
        {
            get
            {
                return (RVO.Count > 0) ? true : false;
            }
        }
        public List<FileItem> RVO { get; set; }

        public bool ExistsPR
        {
            get
            {
                return (PR.Count > 0) ? true : false;
            }
        }
        public List<FileItem> PR { get; set; }

        public bool ExistsRplus
        {
            get
            {
                return (Rplus.Count > 0) ? true : false;
            }
        }
        public List<FileItem> Rplus { get; set; }

        public bool ExistsPFplus
        {
            get
            {
                return (PFplus.Count > 0) ? true : false;
            }
        }
        public List<FileItem> PFplus { get; set; }


        public int MapRows
        {
            get
            {
                return R.Count() + PF.Count() + RVO.Count() + PR.Count() + Rplus.Count() + PFplus.Count();
            }
        }



    }
}
