using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGCont.Data;
using SGCont.Models;
using SGCont.ViewModels;

namespace SGCont.Utils
{
    public class LicenciaService
    {
        private static LicenciaService _service;
        private Licencia _licencia;

        IConfiguration _config;
        IHostingEnvironment _enviroment;

        public LicenciaService(IConfiguration config, IHostingEnvironment enviroment)
        {
            _config = config;
            _enviroment = enviroment;
        }

        // public static LicenciaService GetService()
        // {
        //     if (_service == null)
        //     {
        //         _service = new LicenciaService();
        //     }
        //     return _service;
        // }

        public Licencia Licencia
        {
            get
            {
                if (_licencia == null)
                {
                    DbContextOptions<SGContDbContext> options = new DbContextOptionsBuilder<SGContDbContext>()
                        .UseNpgsql(_config.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("SGCont")).Options;
                    using (var db = new SGContDbContext(options))
                    {
                        _licencia = db.Set<Licencia>().FirstOrDefault();
                    }
                }
                return _licencia;
            }
            set
            {
                _licencia = value;
            }
        }

        public bool Eliminar()
        {
            DbContextOptions<SGContDbContext> options = new DbContextOptionsBuilder<SGContDbContext>()
                        .UseNpgsql(_config.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("SGCont")).Options;
            using (var _db = new SGContDbContext(options))
            {
                var licencia = _db.Set<Licencia>().SingleOrDefault();
                if (licencia == null)
                {
                    return false;
                }
                var path = _enviroment.ContentRootPath;
                System.IO.File.Delete(Path.Combine(path, "licencia.lic"));
                _db.Remove(licencia);
                _db.SaveChanges();
            }
            Licencia = null;
            return true;
        }

    }
}