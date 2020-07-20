namespace SGCont.Models
 {
    public enum Tipo {
        Marco,
        Compra,
        Venta,
        Donación,
        Suministro,
        Deposito,
        Prestación_de_Servicio,
        Agencia,
        Comisión,
        Consignación,
        Arrendamiento,
        Transporte,

    }

    public enum Estado {
        Nuevo,
        Circulando,
        Aprobado,
        No_Aprobado,
        Vigente,
        Cancelado,
        Vencido,
        Revision,
        SinEstado
    }
    public enum EstadoSolicitud {
        Nueva,
        Aprobada,
        NoAprobada,
        Cancelada,
        Pagada
    }
    public enum FormaDePago {
        Transferencia_Bancaria,
        Cheque_Bancario,
        Efectivo

    }
    public enum NombreSucursal {
        Sin_Definir,
        Bandec,
        BPA,
        Banco_Metropolitano,
        BFI
    }
    public enum Moneda {
        Sin_Definir,
        USD,
        CUP,
        CUC
    }
    public enum Sector {
        Sin_Definir,
        Estatal,
        Cooperativo,
        TCP,
        PYME
    }
    public enum Sexo
    {
        Sin_Definir,
        M,
        F,
    }
     public enum ColorDePiel : int {
        Sin_Definir,
        Blanca,
        Negra,
        Mestiza,
    }
      public enum ColorDeOjos {
        Sin_Definir,
        Azules,
        Verdes,
        Negros,
        Marron,
        Pardos
    }
     public enum TallaDeCamisa {
        Sin_Definir,
        S,
        M,
        X,
        L,
        XL,
        XXL,
        XXXL
    }
}