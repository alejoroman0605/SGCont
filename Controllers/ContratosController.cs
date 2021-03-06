using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGCont.Data;
using SGCont.Dtos;
using SGCont.Models;

namespace SGCont.Controllers {
    [Route ("SGCont/[controller]")]
    [ApiController]
    public class ContratosController : Controller {
        private IHostingEnvironment _hostingEnvironment;
        private readonly SGContDbContext context;
        public ContratosController (SGContDbContext context, IHostingEnvironment environment) {

            this.context = context;
            _hostingEnvironment = environment;
        }

        // GET SGCont/Contratos/tipoTramite(Oferta o contrato)
        [HttpGet]
        public ActionResult GetAll (string tipoTramite, string filtro, bool cliente) {
            var trabajadores = context.Trabajadores.ToList ();
            var contratoId_DepartamentoId = context.ContratoId_DepartamentoId.Include (d => d.Departamento).ToList ();
            var espExternoId_ContratoId = context.EspExternoId_ContratoId.Include (d => d.EspecialistaExterno).ToList ();
            DateTime FechaPorDefecto = new DateTime (0001, 01, 01);
            var contratos = context.Contratos.Include (c => c.Entidad).Include (c => c.Montos).Select (c => new {
                Id = c.Id,
                    Nombre = c.Nombre,
                    Tipo = c.Tipo,
                    TipoNombre = c.Tipo.ToString (),
                    AdminContrato = trabajadores.FirstOrDefault (t => t.Id == c.AdminContratoId),
                    Departamentos = contratoId_DepartamentoId.Where (d => d.ContratoId == c.Id).Select (d => new {
                        Id = d.Departamento.Id,
                            Nombre = d.Departamento.Nombre
                    }),
                    ObjetoDeContrato = c.ObjetoDeContrato,
                    Numero = c.Numero,
                    Montos = c.Montos.Select (d => new {
                        ContratoId = d.ContratoId,
                            Cantidad = d.Cantidad,
                            Moneda = d.Moneda,
                            NombreString = d.Moneda.ToString ()
                    }),
                    FechaDeRecepcion = c.FechaDeRecepcion,
                    FechaDeVenOferta = c.FechaDeVenOferta,
                    FechaVenContrato = c.FechaVenContrato,
                    FechaDeFirmado = c.FechaDeFirmado,
                    FechaDeRece = c.FechaDeRecepcion.ToString ("dd/MM/yyyy"),
                    FechaDeVenOfer = c.FechaDeVenOferta.ToString ("dd/MM/yyyy"),
                    FechaVenCont = c.FechaVenContrato.ToString ("dd/MM/yyyy"),
                    TerminoDePago = c.TerminoDePago,
                    TerminoDePagoDet = c.TerminoDePago / 30 + " Meses y " + c.TerminoDePago % 30 + " Días",
                    Estado = c.Estado,
                    EstadoNombre = c.Estado.ToString (),
                    AprobJuridico = c.AprobJuridico,
                    AprobEconomico = c.AprobEconomico,
                    Cliente = c.Cliente,
                    AprobComitContratacion = c.AprobComitContratacion,
                    OfertVence = (c.FechaDeVenOferta - DateTime.Now).Days,
                    ContVence = (c.FechaVenContrato - DateTime.Now).Days,
                    FormasDePago = context.ContratoId_FormaPagoId.Where (t => t.ContratoId == c.Id).Select (f => new {
                        Id = f.FormaDePago,
                            nombre = f.FormaDePago.ToString ()
                    }),
                    EspecialistasExternos = espExternoId_ContratoId.Where (s => s.ContratoId == c.Id).Select (e => new {
                        Id = e.EspecialistaExterno.Id,
                            NombreCompleto = e.EspecialistaExterno.NombreCompleto
                    }),
                    Entidad = context.Entidades.Include (e => e.CuentasBancarias).Include (e => e.Telefonos).Where (s => s.Id == c.EntidadId).Select (e => new {
                        Id = e.Id,
                            Nombre = e.Nombre,
                            Siglas = e.Siglas,
                            Codigo = e.Codigo,
                            Direccion = e.Direccion,
                            Nit = e.Nit,
                            Fax = e.Fax,
                            Sector = e.Sector,
                            SectorNombre = e.Sector.ToString (),
                            Correo = e.Correo,
                            ObjetoSocial = e.ObjetoSocial,
                            Telefonos = e.Telefonos,
                            CantTelefonos = e.Telefonos.Count (),
                            CuentasBancarias = e.CuentasBancarias.Select (b => new {
                                NumeroCuenta = b.NumeroCuenta,
                                    NumeroSucursal = b.NumeroSucursal,
                                    NombreSucursal = b.NombreSucursal,
                                    NombreSucursalString = b.NombreSucursal.ToString (),
                                    Moneda = b.Moneda,
                                    MonedaString = b.Moneda.ToString ()
                            }),
                            CantCuentasBancarias = e.CuentasBancarias.Count ()
                    }),
            });

            if (tipoTramite == "oferta") {
                contratos = contratos.Where (c => c.FechaDeFirmado == FechaPorDefecto && c.AprobComitContratacion == false && c.Estado != Estado.Aprobado);
                var tiempoVenOfertas = context.TiempoVenOfertas.ToList ();
                if (tiempoVenOfertas.Count () > 0) {
                    var tiempoVenOferta = tiempoVenOfertas[0];
                    if (filtro != null) {
                        if (filtro == "ofertaTiempo") {
                            contratos = contratos.Where (c => c.OfertVence > tiempoVenOferta.OfertaTiempo);
                        }
                        if (filtro == "ofertasProxVencer") {
                            contratos = contratos.Where (c => c.OfertVence > tiempoVenOferta.OfertasProxVencDesde && c.OfertVence <= tiempoVenOferta.OfertasProxVencHasta);
                        }
                        if (filtro == "ofertasCasiVenc") {
                            contratos = contratos.Where (c => c.OfertVence >= tiempoVenOferta.OfertasCasiVencDesde && c.OfertVence <= tiempoVenOferta.OfertasCasiVencHasta);
                        }
                        if (filtro == "ofertasVenc") {
                            contratos = contratos.Where (c => c.OfertVence < tiempoVenOferta.OfertasVencidas);
                        }
                    }

                }
            }
            if (tipoTramite == "contrato") {
                var tiempoVenContratos = context.TiempoVenContratos.ToList ();
                var tiempoVenContrato = tiempoVenContratos[0];
                contratos = contratos.Where (c => c.FechaDeFirmado != FechaPorDefecto && c.AprobComitContratacion == true && c.Estado == Estado.Aprobado);
                if (filtro == "contratoTiempo") {
                    contratos = contratos.Where (c => c.ContVence > tiempoVenContrato.ContratoTiempo);
                }
                if (filtro == "contratosProxVencer") {
                    contratos = contratos.Where (c => c.ContVence > tiempoVenContrato.ContratosProxVencerDesde && c.ContVence <= tiempoVenContrato.ContratosProxVencerHasta);
                }
                if (filtro == "contratosCasiVenc") {
                    contratos = contratos.Where (c => c.ContVence >= tiempoVenContrato.ContratosCasiVencDesde && c.ContVence <= tiempoVenContrato.ContratosCasiVencHasta);
                }
                if (filtro == "contratosVenc") {
                    contratos = contratos.Where (c => c.ContVence < tiempoVenContrato.ContratosVencidos);
                }
            }
            if (cliente == true) {
                contratos = contratos.Where (c => c.Cliente == true);
            } else {
                contratos = contratos.Where (c => c.Cliente == false);
            }
            return Ok (contratos);
        }

        // GET: SGCont/Contratos/Id
        [HttpGet ("{id}", Name = "GetContrato")]
        public IActionResult GetbyId (int id) {
            var contrato = context.Contratos.FirstOrDefault (c => c.Id == id);
            if (contrato == null) {
                return NotFound ();
            }
            return Ok (contrato);

        }

        // POST SGCont/Contratos
        [HttpPost]
        public async Task<IActionResult> POST (ContratoDto contratoDto) {
            if (ModelState.IsValid) {
                var contrato = new Contrato {
                    Id = contratoDto.Id,
                    Nombre = contratoDto.Nombre,
                    Tipo = contratoDto.Tipo,
                    AdminContratoId = contratoDto.AdminContrato,
                    EntidadId = contratoDto.Entidad,
                    ObjetoDeContrato = contratoDto.ObjetoDeContrato,
                    Numero = contratoDto.Numero,
                    TerminoDePago = contratoDto.TerminoDePago,
                    Cliente = contratoDto.Cliente,
                    Estado = Estado.Circulando
                };
                if (contratoDto.FechaDeRecepcion != null) {
                    contrato.FechaDeRecepcion = contratoDto.FechaDeRecepcion;
                } else {
                    contrato.FechaDeRecepcion = DateTime.Now;
                }
                if (contratoDto.FechaDeVenOferta != null) {
                    contrato.FechaDeVenOferta = contratoDto.FechaDeVenOferta;
                } else {
                    contrato.FechaDeVenOferta = DateTime.Now.AddDays (20);
                }
                context.Contratos.Add (contrato);
                context.SaveChanges ();

                if (contratoDto.Montos != null) {
                    foreach (var item in contratoDto.Montos) {
                    var monto = new Monto {
                    Cantidad = item.Cantidad,
                    Moneda = item.Moneda,
                    ContratoId = contrato.Id
                        };
                        context.Montos.Add (monto);
                    }
                    context.SaveChanges ();
                }
                foreach (var item in contratoDto.FormasDePago) {
                    var contratoId_FormaPagoId = new ContratoId_FormaPagoId {
                        ContratoId = contrato.Id,
                        FormaDePago = item
                    };
                    context.ContratoId_FormaPagoId.Add (contratoId_FormaPagoId);
                    context.SaveChanges ();
                }
                // Subir el Fichero del Contrato
                // var uploads = Path.Combine (_hostingEnvironment.WebRootPath, "uploadContratos");
                // if (file != null) {
                //     var filePath = Path.Combine (uploads, file.FileName);
                //     using (var fileStream = new FileStream (filePath, FileMode.Create)) {
                //        await file.CopyToAsync (fileStream);
                //     }
                //     contrato.FilePath=filePath;
                // }

                // Agregar Departamentos de los Dictaminadores 
                if (contratoDto.Departamentos != null) {
                    foreach (var item in contratoDto.Departamentos) {
                    var contratoId_DepartamentoId = new ContratoId_DepartamentoId {
                    ContratoId = contrato.Id,
                    DepartamentoId = item
                        };
                        context.ContratoId_DepartamentoId.Add (contratoId_DepartamentoId);
                        context.SaveChanges ();
                    }
                }

                //Agregar Especialistas externos como Dictaminador/es del contrato 
                if (contratoDto.EspecialistasExternos != null) {
                    foreach (var item in contratoDto.EspecialistasExternos) {
                    var espExternoId_ContratoId = new EspExternoId_ContratoId {
                    ContratoId = contrato.Id,
                    EspecialistaExternoId = item
                        };
                        context.EspExternoId_ContratoId.Add (espExternoId_ContratoId);
                        context.SaveChanges ();
                    }
                }
                var HistoricoEstadoContrato = new HistoricoEstadoContrato {
                    ContratoId = contrato.Id,
                    Estado = Estado.Circulando,
                    Fecha = DateTime.Now,
                    Usuario = contratoDto.Usuario,
                };
                context.Add (HistoricoEstadoContrato);
                context.SaveChanges ();
                return new CreatedAtRouteResult ("GetContrato", new { id = contrato.Id });
            }
            return BadRequest (ModelState);
        }

        // PUT SGCont/contrato/id
        [HttpPut ("{id}")]
        public IActionResult PUT ([FromBody] ContratoEditDto contrato, int id) {

            if (contrato.Id != id) {
                return BadRequest (ModelState);
            }
            var c = context.Contratos.FirstOrDefault (s => s.Id == id);
            c.Id = contrato.Id;
            c.Nombre = contrato.Nombre;
            c.Tipo = contrato.Tipo;
            c.AdminContratoId = contrato.AdminContrato;
            c.EntidadId = contrato.Entidad.Id;
            c.ObjetoDeContrato = contrato.ObjetoDeContrato;
            c.Numero = contrato.Numero;
            c.TerminoDePago = contrato.TerminoDePago;

            if (contrato.FechaDeRecepcion != null) {
                c.FechaDeRecepcion = contrato.FechaDeRecepcion;
            } else {
                c.FechaDeRecepcion = DateTime.Now;
            }
            if (contrato.FechaDeVenOferta != null) {
                c.FechaDeVenOferta = contrato.FechaDeVenOferta;
            } else {
                c.FechaDeVenOferta = DateTime.Now.AddDays (20);
            }
            context.Entry (c).State = EntityState.Modified;

            //   Agregar monto por monedas
            if (contrato.Montos != null) {
                var montos = context.Montos.Where (s => s.ContratoId == contrato.Id);
                foreach (var item in montos) {
                    context.Montos.Remove (item);
                }
                foreach (var item in contrato.Montos) {
                    var monto = new Monto {
                        Cantidad = item.Cantidad,
                        Moneda = item.Moneda,
                        ContratoId = contrato.Id
                    };
                    context.Montos.Add (monto);
                    context.SaveChanges ();
                }
            }

            if (contrato.FormasDePago != null) {
                var formasDePago = context.ContratoId_FormaPagoId.Where (s => s.ContratoId == contrato.Id);
                foreach (var item in formasDePago) {
                    context.ContratoId_FormaPagoId.Remove (item);
                }
            }
            foreach (var item in contrato.FormasDePago) {
                var contratoId_FormaPagoId = new ContratoId_FormaPagoId {
                    ContratoId = contrato.Id,
                    FormaDePago = item
                };
                context.ContratoId_FormaPagoId.Add (contratoId_FormaPagoId);
                context.SaveChanges ();
            }

            // Agregar Departamentos del contrato 
            if (contrato.Departamentos != null) {
                var departamentos = context.ContratoId_DepartamentoId.Where (s => s.ContratoId == contrato.Id);
                foreach (var item in departamentos) {
                    context.ContratoId_DepartamentoId.Remove (item);
                }
                foreach (var item in contrato.Departamentos) {
                    var contratoId_DepartamentoId = new ContratoId_DepartamentoId {
                        ContratoId = contrato.Id,
                        DepartamentoId = item.Id
                    };
                    context.ContratoId_DepartamentoId.Add (contratoId_DepartamentoId);
                    context.SaveChanges ();
                }
            } else {
                return BadRequest ($"Tienen que dictaminar el contrato el económico y el jurídico");
            }

            //Agregar Especialistas externos como Dictaminador/es del contrato 
            if (contrato.EspecialistasExternos != null) {
                var espExternos = context.EspExternoId_ContratoId.Where (s => s.ContratoId == contrato.Id);
                foreach (var item in espExternos) {
                    context.EspExternoId_ContratoId.Remove (item);
                }
                foreach (var item in contrato.EspecialistasExternos) {
                    var espExternoId_ContratoId = new EspExternoId_ContratoId {
                        ContratoId = contrato.Id,
                        EspecialistaExternoId = item.Id
                    };
                    context.EspExternoId_ContratoId.Add (espExternoId_ContratoId);
                    context.SaveChanges ();
                }
            }
            var HistoricoEstadoContrato = new HistoricoEstadoContrato {
                ContratoId = contrato.Id,
                Estado = contrato.Estado,
                Fecha = DateTime.Now,
                Usuario = contrato.Usuario,
            };
            context.Add (HistoricoEstadoContrato);
            context.SaveChanges ();
            return Ok ();
        }

        // DELETE SGCont/contrato/id
        [HttpDelete ("{id}")]
        public IActionResult Delete (int id) {
            var contrato = context.Contratos.FirstOrDefault (s => s.Id == id);
            if (contrato == null) {
                return NotFound ();
            }
            context.Contratos.Remove (contrato);
            context.SaveChanges ();
            return Ok (contrato);
        }
      
        // PUT SGCont/contratos/AprobContrato/id
        [HttpPut ("/SGCont/contratos/AprobContrato/{id}")]
        public IActionResult AprobContrato ([FromBody] AproContratoDto aprobarContratoDto, int id) {
            if (aprobarContratoDto.ContratoId != id) {
                return BadRequest (ModelState);
            }
            var c = context.Contratos.Find (id);
            if (c != null && aprobarContratoDto.roles != null) {
                if (aprobarContratoDto.roles.Contains ("economico")) {
                    c.AprobEconomico = true;
                } else if (aprobarContratoDto.roles.Contains ("juridico")) {
                    c.AprobJuridico = true;
                } else if (aprobarContratoDto.roles.Contains ("comite de contratacion") && aprobarContratoDto.FechaDeFirmado != null) {
                    c.AprobComitContratacion = true;
                    c.FechaDeFirmado = aprobarContratoDto.FechaDeFirmado;
                    c.FechaVenContrato = aprobarContratoDto.FechaDeVencimiento;
                    c.Estado = Estado.Aprobado;
                }
                context.Update (c);
                context.SaveChanges (); 
                return Ok ();
            }
            return NotFound ();
        }

        // GET: SGCont/contratos/Tipos
        [HttpGet ("/SGCont/contratos/Tipos")]
        public IActionResult GetAllTiposContratos () {
            var tipo = new List<dynamic> () {
                new { Id = Tipo.Marco, Nombre = Tipo.Marco.ToString () },
                new { Id = Tipo.Compra, Nombre = Tipo.Compra.ToString () },
                new { Id = Tipo.Venta, Nombre = Tipo.Venta.ToString () },
                new { Id = Tipo.Agencia, Nombre = Tipo.Agencia.ToString () },
                new { Id = Tipo.Arrendamiento, Nombre = Tipo.Arrendamiento.ToString () },
                new { Id = Tipo.Comisión, Nombre = Tipo.Comisión.ToString () },
                new { Id = Tipo.Consignación, Nombre = Tipo.Consignación.ToString () },
                new { Id = Tipo.Deposito, Nombre = Tipo.Deposito.ToString () },
                new { Id = Tipo.Donación, Nombre = Tipo.Donación.ToString () },
                new { Id = Tipo.Prestación_de_Servicio, Nombre = "Prestación de Servicio" },
                new { Id = Tipo.Suministro, Nombre = Tipo.Suministro.ToString () },
                new { Id = Tipo.Transporte, Nombre = Tipo.Transporte.ToString () },
            };
            return Ok (tipo);
        }
        // GET: SGCont/contratos/Tipos
        [HttpGet ("/SGCont/contratos/Estados")]
        public IActionResult GetAllEstadosContratos () {
            var estadosContratos = new List<dynamic> () {
                new { Id = Estado.Nuevo, Nombre = Estado.Nuevo.ToString () },
                new { Id = Estado.Circulando, Nombre = Estado.Circulando.ToString () },
                new { Id = Estado.Aprobado, Nombre = Estado.Aprobado.ToString () },
                new { Id = Estado.No_Aprobado, Nombre = "No Aprobado" },
                new { Id = Estado.Vigente, Nombre = Estado.Vigente.ToString () },
                new { Id = Estado.Cancelado, Nombre = Estado.Cancelado.ToString () },
                new { Id = Estado.Vencido, Nombre = Estado.Vencido.ToString () },
                new { Id = Estado.Revision, Nombre = Estado.Revision.ToString () },
                new { Id = Estado.SinEstado, Nombre = "Sin Estado" },
            };
            return Ok (estadosContratos);
        }

        // GET: SGCont/contratos/VencimientoContrato 
        [HttpGet ("/SGCont/contratos/VencimientoContrato")]
        public IActionResult GetVencimientoContrato (bool cliente) {
            DateTime FechaPorDefecto = new DateTime (0001, 01, 01);
            List<int> cantSegunFecha = new List<int> ();
            var tiempoVenContratos = context.TiempoVenContratos.ToList ();
            var tiempoVenContrato = tiempoVenContratos[0];
            var contratos = context.Contratos
                .Where (c => c.FechaDeFirmado != FechaPorDefecto &&
                    c.AprobComitContratacion == true && c.Estado == Estado.Aprobado);
            if (cliente == true) {
                contratos = contratos.Where (c => c.Cliente == true);
            } else {
                contratos = contratos.Where (c => c.Cliente == false);
            }
            // Contratos vencidos           
            cantSegunFecha.Add (contratos
                .Where (c => (c.FechaVenContrato - DateTime.Now).Days < tiempoVenContrato.ContratosVencidos).Count ());

            // Contratos Casi Vencidas
            cantSegunFecha.Add (contratos
                .Where (c => (c.FechaVenContrato - DateTime.Now).Days >= tiempoVenContrato.ContratosCasiVencDesde && (c.FechaVenContrato - DateTime.Now).Days <= tiempoVenContrato.ContratosCasiVencHasta).Count ());

            // Contratos Próximos a Vencer
            cantSegunFecha.Add (contratos
                .Where (c => (c.FechaVenContrato - DateTime.Now).Days > tiempoVenContrato.ContratosProxVencerDesde && (c.FechaVenContrato - DateTime.Now).Days <= tiempoVenContrato.ContratosProxVencerHasta).Count ());

            // Contratos OK
            cantSegunFecha.Add (contratos.Where (c => (c.FechaVenContrato - DateTime.Now).Days > tiempoVenContrato.ContratoTiempo).Count ());

            return Ok (cantSegunFecha);
        }

        // GET: SGCont/contratos/VencimientoOferta 
        [HttpGet ("/SGCont/contratos/VencimientoOferta")]
        public IActionResult GetVencimientoOferta (bool cliente) {
            DateTime FechaPorDefecto = new DateTime (0001, 01, 01);
            List<int> cantSegunFecha = new List<int> ();
            var tiempoVenOfertas = context.TiempoVenOfertas.ToList ();
            var tiempoVenOferta = tiempoVenOfertas[0];
            var ofertas = context.Contratos
                .Where (c => c.FechaDeFirmado == FechaPorDefecto &&
                    c.AprobComitContratacion == false && c.Estado != Estado.Aprobado);
            if (cliente == true) {
                ofertas = ofertas.Where (c => c.Cliente == true);
            } else {
                ofertas = ofertas.Where (c => c.Cliente == false);
            }
            // Ofertas vencidos           
            cantSegunFecha.Add (ofertas
                .Where (c => (c.FechaDeVenOferta - DateTime.Now).Days < tiempoVenOferta.OfertasVencidas).Count ());

            // Ofertas Casi Vencidas
            cantSegunFecha.Add (ofertas
                .Where (c => (c.FechaDeVenOferta - DateTime.Now).Days >= tiempoVenOferta.OfertasCasiVencDesde && (c.FechaDeVenOferta - DateTime.Now).Days <= tiempoVenOferta.OfertasCasiVencHasta).Count ());

            // Ofertas Próximos a Vencer
            cantSegunFecha.Add (ofertas
                .Where (c => (c.FechaDeVenOferta - DateTime.Now).Days > tiempoVenOferta.OfertasProxVencDesde && (c.FechaDeVenOferta - DateTime.Now).Days <= tiempoVenOferta.OfertasProxVencHasta).Count ());

            // Ofertas OK
            cantSegunFecha.Add (ofertas.Where (c => (c.FechaDeVenOferta - DateTime.Now).Days > tiempoVenOferta.OfertaTiempo).Count ());

            return Ok (cantSegunFecha);
        }

        //Post :SGCont/contratos/UploadFile
        [HttpPost ("/SGCont/contratos/UploadFile/{id}")]
        public async Task<IActionResult> UploadFile (IFormFile file, int id) {
            var contrato = context.Contratos.FirstOrDefault (s => s.Id == id);
            var adminContrato = context.AdminContratos.FirstOrDefault (c => c.AdminContratoId == contrato.AdminContratoId);
            var departamento = context.Departamentos.FirstOrDefault (d => d.Id == adminContrato.DepartamentoId);
            if (contrato == null) {
                return NotFound ($"El Contrato no se encuentra");
            }
            if (file != null) {
                string folderName = Path.Combine (_hostingEnvironment.WebRootPath, "uploadContratos");
                string subFolder = System.IO.Path.Combine (folderName, departamento.Nombre);
                if (!Directory.Exists (subFolder)) {
                    System.IO.Directory.CreateDirectory (subFolder);
                }
                var filePath = Path.Combine (subFolder, file.FileName);
                using (var fileStream = new FileStream (filePath, FileMode.Create)) {
                    await file.CopyToAsync (fileStream);
                }
                contrato.FilePath = filePath;
                context.SaveChanges ();
                return Ok ();
            } else {
                return NotFound ($"El archivo es null");
            }
        }

        private Dictionary<string, string> GetMimeTypes () {
            return new Dictionary<string, string> { { ".txt", "text/plain" },
                { ".pdf", "application/pdf" },
                { ".doc", "application/vnd.ms-word" },
                { ".docx", "application/vnd.ms-word" },
                { ".xls", "application/vnd.ms-excel" },
                { ".xlsx", "application/vnd.openxmlformats officedocument.spreadsheetml.sheet" },
                { ".png", "image/png" },
                { ".jpg", "image/jpeg" },
                { ".jpeg", "image/jpeg" },
                { ".gif", "image/gif" },
                { ".csv", "text/csv" }
            };
        }

        //Post :SGCont/contratos/downloadFile
        [HttpGet ("/SGCont/contratos/DownloadFile/{id}")]
        public async Task<IActionResult> DownloadFile (int id) {
            var contrato = context.Contratos.FirstOrDefault (c => c.Id == id);
            if (contrato.FilePath != null) {
                var path = contrato.FilePath;
                var memory = new MemoryStream ();
                using (var stream = new FileStream (path, FileMode.Open)) { await stream.CopyToAsync (memory); }
                memory.Position = 0;
                var ext = Path.GetExtension (path).ToLowerInvariant ();
                return File (memory, GetMimeTypes () [ext], Path.GetFileName (path));
            }
            return NotFound ($"No tiene un documento guardado");

        }

        // GET: SGCont/contratos/Dashboard 
        [HttpGet ("/SGCont/contratos/Dashboard")]
        public async Task<IActionResult> Dashboard () {
            var dashboard = new Dashboard ();
            DateTime FechaPorDefecto = new DateTime (0001, 01, 01);
            var cantOfertas_x_Mes = new int[12];
            var cantOfertVen_x_Mes = new int[12];
            var contratosProximosVencer = new int[5];
            var tiempoVenOfertas = context.TiempoVenOfertas.ToList ();
            var tiempoVenOferta = tiempoVenOfertas[0];

            var ofertasContratos = context.Contratos.Where (c => c.FechaDeRecepcion.Year == DateTime.Now.Year);
            var contratos = context.Contratos.Where (c => c.FechaDeFirmado != FechaPorDefecto &&
                c.AprobComitContratacion == true && c.Estado == Estado.Aprobado).ToList ();

            var ofertas = context.Contratos.Where (c => c.FechaDeFirmado == FechaPorDefecto &&
                c.AprobComitContratacion == false && c.Estado != Estado.Aprobado);

            if (ofertas != null) {
                // Grafico Lineal Ofertas //
                for (int i = 0; i < 12; i++) {
                    cantOfertas_x_Mes[i] = ofertas.Where (c => c.Estado != Estado.Cancelado &&
                        c.Estado != Estado.No_Aprobado && c.Estado != Estado.SinEstado && c.FechaDeRecepcion.Month == i + 1).Count ();
                    cantOfertVen_x_Mes[i] = ofertas
                        .Where (c => c.FechaDeRecepcion.Month == i + 1 &&
                            (c.FechaDeVenOferta - DateTime.Now).Days < tiempoVenOferta.OfertasVencidas).Count ();
                }
                dashboard.OfertasProceso = cantOfertas_x_Mes;
                dashboard.OfertasVencidas = cantOfertVen_x_Mes;
                // Grafico Lineal Ofertas//

                // Tabla Ofertas//
                dashboard.OfertasEnProceso = ofertas.Where (c => c.Estado != Estado.Cancelado &&
                    c.Estado != Estado.No_Aprobado && c.Estado != Estado.SinEstado).Count ();

                dashboard.OfertasVenHastaFecha = ofertas.Where (c => (c.FechaDeVenOferta - DateTime.Now).Days < tiempoVenOferta.OfertasVencidas).Count ();

                dashboard.OfertasVenEsteMes = ofertas.Where (c => c.FechaDeVenOferta.Month == DateTime.Now.Month &&
                    (c.FechaDeVenOferta - DateTime.Now).Days < tiempoVenOferta.OfertasVencidas).Count ();
            }
            dashboard.OfertasProcesadas = ofertasContratos.Where (c => c.FechaDeRecepcion.Year == DateTime.Now.Year).Count ();
            if (contratos != null) {
                double TiempoCirculacion = 0;
                double TiempoCirculacionMes = 0;
                foreach (var item in contratos) {
                    TiempoCirculacion += (item.FechaDeFirmado - item.FechaDeRecepcion).Days;
                }
                foreach (var item in contratos.Where (c => c.FechaDeRecepcion.Month == DateTime.Now.Month)) {
                    TiempoCirculacionMes += (item.FechaDeFirmado - item.FechaDeRecepcion).Days;
                }
                dashboard.PromCircuOferta = Math.Round (TiempoCirculacion / contratos.Count (), 0);
                dashboard.PromCircuOfertaMes = Math.Round (TiempoCirculacionMes / contratos
                    .Where (c => c.FechaDeRecepcion.Month == DateTime.Now.Month).Count (), 0);
                // Tabla Ofertas//

                // Contratos Próximos a Vencer //
                contratosProximosVencer[0] = contratos.Where (c => c.FechaVenContrato.Month == DateTime.Now.Month).Count ();

                contratosProximosVencer[1] = contratos.Where (c => c.FechaVenContrato.Month == (DateTime.Now.Month + 1) ||
                    c.FechaVenContrato.Month == (DateTime.Now.Month + 2) || c.FechaVenContrato.Month == (DateTime.Now.Month + 3)).Count ();

                contratosProximosVencer[2] = contratos.Where (c => c.FechaVenContrato.Month == (DateTime.Now.Month + 1) ||
                    c.FechaVenContrato.Month == (DateTime.Now.Month + 2) || c.FechaVenContrato.Month == (DateTime.Now.Month + 3) ||
                    c.FechaVenContrato.Month == (DateTime.Now.Month + 4) || c.FechaVenContrato.Month == (DateTime.Now.Month + 5) ||
                    c.FechaVenContrato.Month == (DateTime.Now.Month + 6)).Count ();

                contratosProximosVencer[3] = contratos.Where (c => c.FechaVenContrato.Year == DateTime.Now.Year).Count ();
                contratosProximosVencer[4] = contratos.Where (c => c.FechaVenContrato.Year == DateTime.Now.Year + 1).Count ();
                dashboard.ContratosProximosVencer = contratosProximosVencer;
                // Contratos Próximos a Vencer //
            }

            return Ok (dashboard);
        }
    }
}