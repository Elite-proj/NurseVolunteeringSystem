using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace NurseVolunteeringSystem.DataAccess
{
    public class DataAccessLayer
    {
        public readonly IConfiguration _configuration;

        public DataAccessLayer(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        SqlConnection dbconn;
        SqlCommand dbComm;
        SqlDataAdapter dbAdapter;
        DataTable dt;

        #region Admin Area
        #endregion
    }
}
