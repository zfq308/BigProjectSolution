using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BigProject.SpecificModule.Model;

namespace BigProject.SpecificModule.DataInterfaceLayer
{
    public enum EnumStoredProcedures
    {
        getlist,
        AddEntity,
    }

    public interface ISpecificModule_RO_Operations
    {
        DataSet getlist(string p_inputparameter);
    }

    public interface ISpecificModule_WO_Operations
    {
        int AddEntity(DemoModel dm);
    }
}
