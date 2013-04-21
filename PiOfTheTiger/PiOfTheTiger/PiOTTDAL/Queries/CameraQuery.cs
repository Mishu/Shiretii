using PiOTTDAL.Entities;
using PiOTTDAL.Entities.Attributes;
using PiOTTDAL.Queries.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiOTTDAL.Queries
{
    public class CameraQuery : QueryRunner
    {
        public List<Camera> GetAllCamera()
        {
            return GetAll<Camera>();
        }

        public Camera GetCameraByName(string name)
        {
            return GetByAttribute<Camera>(name, typeof(Name));
        }
    }
}
