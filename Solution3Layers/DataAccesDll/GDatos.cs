using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesDll
{
    public abstract class GDatos
    {

#region "Declaracion de Variables"

        protected string MServidor = "";
        protected string MBase = "";
        protected string MUsuario = "";
        protected string MPasword = "";
        protected string MCadenaConexion = "";
        protected IDbConnection MConexion;


#endregion

#region "Getters and Setter"

        public string Servidor
        {
            get { return MServidor; }
            set { MServidor = value; }
        }

        public string Base
        {
            get { return MBase; }
            set { MBase = value; }
        }

        public string Usuario
        {
            get { return MUsuario;}
            set { MUsuario = value; }
        }

        public string Password
        {
            get { return MPasword; }
            set { MPasword = value; }
        }

        public abstract string CadenaConexion { get; set; }



#endregion

#region "Private data acces"

        protected IDbConnection Conexion
        {
            get
            {
                if (MConexion == null)
                {
                    MConexion = CrearConexion(CadenaConexion);
                }

                if (MConexion.State != ConnectionState.Open)
                {
                    MConexion.Open();
                }
                return MConexion;
            }
        }
#endregion

#region "Lecturas"

        /*Creamos ahora los metodos para hacer lecturas a la fuente de datos, lo
         * hacenmos en esta clase porque son metodos que pueden implementar tal cual
         * las clases hijas , en el caso de los datareader que son muy especificos
         * dependiendo del driver utilizado, se usara el objecto IDataReader que es una interfaz
         * de procedimiento muy general. */

        //obtiene un data set a traves de un procedimento almacenado

        public DataSet TraerDataSet(string procedimientoAlmacenado)
        {
            var mDataSet = new DataSet();
            CrearDataAdapter(procedimientoAlmacenado).Fill(mDataSet);
            return mDataSet;
        }
        //obtiene un dataset a partir de sus procemientos almacenados y sus parametros
        public DataSet TraerDataSet(string procedimientoAlmacenado, params object[] args)
        {
            var mDataSet = new DataSet();
            CrearDataAdapter(procedimientoAlmacenado, args).Fill(mDataSet);
            return mDataSet;
        }

        //obtiene un dataset a partir de un query sql.

        public DataSet TraerDataSetSql(string commandoSql)
        {
            var mDataSet = new DataSet();
            crearDataAdapter(commandoSql).Fill(mDataSet);
            return mDataSet;
        }



        //data Table

        //Obtiene un data tabla a partir de un procedimiento almacenado

        public DataTable TraerDataTable(string procedimientoAlmacenado)
        {
            return TraerDataSet(procedimientoAlmacenado).Tables[0].Copy();
        }

        //Obtiene un dataset a partir de un procedimiento almacenado y sus parametros;


        public DataTable TraerDataTable(string procedimientoAlmacenado, params Object[] args)
        {
            return TraerDataSet(procedimientoAlmacenado, args).Tables[0].Copy();
        }

        //obtiene un datatable a partir de un query sql

        public DataTable TraerDataTableSql(string commandoSql)
        {
            return TraerDataSetSql(commandoSql).Tables[0].Copy();
        }

        //Data Reader

        //Obtiene un DataReader a partir de un procedimiento Almacenado


        public IDataReader TraerDataReader(string procemiendoAlmacenado)
        {
            var com = Comando(procemiendoAlmacenado);
            return com.executeReader;
        }

        //obtiene un datareader a partir de sus procedimientos almacenados y sus parametros

        public IDataReader TraerDataReader(string procedimientoAlmacenado, params Object[] args)
        {
            var com=Comando(com, args);
            return com.ExecuteReader;
        }

        //Obtener un datareader a partir de un procedimiento almacenado

        public IDataReader TraerDataReaderSql(string procedimientoAlmacenado)
        {
            var com = Commando(procedimientoAlmacenado);
            return com.ExecuteReader;
        }

        public object TraerValorOutput(string procedimientoAlmacenado)
        {
            var com = Comando(procedimientoAlmacenado);
            com.ExecuteNonQuery;
            Object resp = null;

            //Recorrer los parametros del SP

            foreach (IDbDataParameter par in com.Parameters)
            {

                //Si tiene parametros de tipo IO retornar ese valor

                if (par.Direction == ParameterDirection.InputOutput || par.Direction == ParameterDirection.Output)
                    resp = par.Value;
                

            }

            return resp;


        }

        //obtener un valor a partir de un procedimiento almacenado y sus parametros


        public object TraerValorOutput(string procedimientoAlmacenado, params Object[] args)
        {
            var com = Comando(procedimientoAlmacenado);
            CargarParametros(com, args);
            com.ExecuteNonQuery();
            Object resp = null;
            foreach (IDbDataParameter par  in com.Parameters)
            {
                if (par.Direction == ParameterDirection.InputOutput ||
                    par.Direction == ParameterDirection.Output)
                {
                    resp = par.Value;
                }
                
            }

            return resp;
        }


        //obtiene un valor a partir de un procedimiento almacenado

        public object TraerValorOutputSql(string comandoSql)
        {
            var com = ComandoSQL(comandoSql);
            com.ExecuteNonQuery;
            object resp = null;

            foreach (IDbDataParameter par in com.Parameters)
            {
                if(par.Direction==ParameterDirection.InputOutput||par.Direction==ParameterDirection.Output)
                {
                    resp = par.Value;
                }

            }
            return resp;
        }

        //Obtiene un valor de una funcion escalar a partir de un procedimiento almacenado

        public object TraerValorEscalar(string procedimientoAlmacenado)
        {
            var com = Comando(procedimientoAlmacenado);
            return com.ExecuteEscalar();
        }

        //obtiene un valor de una funcion escalar a partir de un procedimiento lamacenado, con Params de entrada

        public object TraerValorEscalar(string procedimientoAlmacenado, params Object[] args)
        {
            var com = Comando(procedimientoAlmacenado);
            CargarParametros(com, args);
            return com.ExecuteEscalar;
        }

        //Obtiene un valor de una fucnion escalar a partir de un sql

        public object TraerValorEscalarSql(string comandoSql)
        {
            var com = comandoSql(comandoSql);
            return com.ExecuteEscalar;
        }

    #endregion

        /* el siguiente bloque es para ejecutar procesos que no devuelven valores
         * se tienen varios metodos abstractos, para que las clases derivadas
         * esten obligadas a implementarlas a su manera en un modo especifico
         * ya que los objetos command, connection, dataadapter
         * son muy especificos ydeben ser implementados por cada una*/

        #region "Acciones"

        protected abstract IDbConnection CrearConexion(string cadena);
        protected abstract IDbCommand Comando(string procedimientoAlmacenado);
        protected abstract IDbCommand ComandoSql(string comandoSql);
        protected abstract IDbDataAdapter CrearDataAdapter(string procedimientoAlmacenado, params Object[] args);
        protected abstract IDbDataAdapter CrearDataAdapterSql(string comandoSql);
        protected abstract void CargarParametros(IDbCommand comando, Object[] args);


        public bool Autenticar()
        {
            if (Conexion.State != ConnectionState.Open)
            {
                Conexion.Open();
            }
            return true;
        }

        public bool Autenticar(string vUsuario, string vPassword)
        {
            MUsuario = vUsuario;
            MPasword = vPassword;
            MConexion = CrearConexion(CadenaConexion);

            MConexion.Open();
            return true;
        }

        public void CerrarConexion()
        {
            if (Conexion.State != ConnectionState.Closed)
            {
                Conexion.Close();

            }


        }


        //EJECUTA UN PROCEDIMIENTO EN LA BASE DE DATOS

        public int Ejecutar(string procedimientoAlmacenado)
        {
            return ComandoSql(procedimientoAlmacenado).ExecuteNonQuery();
        }

        public int EjecutarSql(string comandoSql)
        {
            return ComandoSql(comandoSql).ExecuteNonQuery();
        }

        //procedimiento almacenado en la base de datos, usando los parametros

        public int Ejecutar(string procedimientoAlmacenado, params Object[] args)
        {
            var com = Comando(procedimientoAlmacenado);
            CargarParametros(com, args);
            var resp = com.ExecuteNonQuery();
            for (var i = 0; i < com.Parameters.Count; i++)
            {
                var par = (IDbDataParameter) com.Parameters[i];
                if (par.Direction == ParameterDirection.InputOutput || par.Direction == ParameterDirection.Output)
                {
                    args.SetValue(par.Value, i-1);
                }
                
            }
            return resp;
        }





        #endregion



        #region "Transacciones"

        protected IDbTransaction MTransaction;
        protected bool EnTransaccion;

        public void IniciarTransaccion()
        {
            try
            {
                MTransaction = Conexion.BeginTransaction();
                EnTransaccion = true;
            }
            finally
            {
                EnTransaccion = false;
                
            }
        }

        public void TerminarTransaccion()
        {
            try
            {
                MTransaction.Commit();
            }
            finally
            {
                MTransaction = null;
                EnTransaccion = false;

            }
        }

        public void AbortarTransaccion()
        {
            try
            {
                MTransaction.Rollback();
            }
           finally
            {
                MTransaction = null;
                EnTransaccion = false;
            }
        }

        #endregion
    }
}
