using DBEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBContext
{
    public interface IProjectRepository
    {
        ResponseBase getProjects();
        ResponseBase getProject(int id);
        ResponseBase Insert(EntityProject project);
    }
}
