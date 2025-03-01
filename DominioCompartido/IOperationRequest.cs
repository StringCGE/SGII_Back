﻿namespace SGII_Back.Dominio.Shared
{
    public interface IOperationRequest
    {
        string Ip { get; }

        public DateTime Fecha { get;}
        public DateTime FechaUTC { get; }

        public IUserEntity? Usuario { get; }
    }

    public interface IOperationRequest<T> : IOperationRequest
    {
        public T Data { get; set; }
 
    }
}