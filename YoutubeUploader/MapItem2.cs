using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeUploader
{
    public class MapItem2
    {
        public string MapFilePath { get; set; }
        public string Name { get; set; }

        public MapItem2()
        {
            R = new List<FileItem2>();
            PF = new List<FileItem2>();
            RVO = new List<FileItem2>();
            PR = new List<FileItem2>();
            Rplus = new List<FileItem2>();
            PFplus = new List<FileItem2>();

        }

        public bool ExistsR
        {
            get
            {
                return (R.Count > 0) ? true : false;
            }
        }
        public List<FileItem2> R { get; set; }

        public bool ExistsPF
        {
            get
            {
                return (PF.Count > 0) ? true : false;
            }
        }
        public List<FileItem2> PF { get; set; }

        public bool ExistsRVO
        {
            get
            {
                return (RVO.Count > 0) ? true : false;
            }
        }
        public List<FileItem2> RVO { get; set; }

        public bool ExistsPR
        {
            get
            {
                return (PR.Count > 0) ? true : false;
            }
        }
        public List<FileItem2> PR { get; set; }

        public bool ExistsRplus
        {
            get
            {
                return (Rplus.Count > 0) ? true : false;
            }
        }
        public List<FileItem2> Rplus { get; set; }

        public bool ExistsPFplus
        {
            get
            {
                return (PFplus.Count > 0) ? true : false;
            }
        }
        public List<FileItem2> PFplus { get; set; }


        public int MapRows
        {
            get
            {
                return R.Count() + PF.Count() + RVO.Count() + PR.Count() + Rplus.Count() + PFplus.Count();
            }
        }



    }
}
