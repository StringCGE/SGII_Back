using Dominio.Infrastructure.Persistence.Repository;
using Microsoft.AspNetCore.Mvc;
using SGII_Back.Dominio.Services;
using System.Net;

namespace Avistador.API.Controllers.AppUsuario
{
    /// <summary>
    /// Reursos para la gestion de AppUsuario
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AppUsuarioController : ControllerBase
    {
        

        /// <summary>
        /// Consulta si existe aquel usuario
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("UserNameExist/{name}")]
        [ProducesResponseType(typeof(IOperationResult<bool>), 200)]
        [ProducesResponseType(typeof(IOperationResult), 204)]
        [ProducesResponseType(typeof(IOperationResult), 404)]
        [ProducesResponseType(typeof(IOperationResult), 500)]
        public async Task<IOperationResult> UserNameExist(string name)
        {
            UserService userService = new UserService(new AuthService(new UserRepository()));
            bool res = await userService.UserNameExist(name);
            IOperationResult<bool> result;
            if (res)
            {
                result = new OperationResult<bool>(
                HttpStatusCode.OK,
                $"El usuario \"{name}\" ya existe",
                true,
                "");
            }
            else
            {
                result = new OperationResult<bool>(
                HttpStatusCode.OK,
                $"El usuario \"{name}\" no existe",
                false,
                "");
            }
            return result;
        }




        /*
        /// <summary>
        /// Crea usuarios de tipo Visitante VisitanteUserRequest
        /// </summary> 
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(VisitanteUserRequest request)//[FromBody] 
        {
            UserRequest userRequest = new UserRequest();
            userRequest.Identification = request.Identification;
            if (request.UrlFoto != null)
            {
                userRequest.UrlFoto = request.UrlFoto;
            }
            else
            {
                userRequest.UrlFoto = "";
            }
           
            userRequest.Email = request.Email;
            userRequest.Password = request.Password;
            userRequest.role = "visitante";
            userRequest.nombre = request.Nombre;
            userRequest.apellido = request.Apellido;
            userRequest.cedula = request.Cedula;
            userRequest.nacimiento = request.Nacimiento;

            UserDTO user;
            try
            {
                user = _userAppService.Create(userRequest);
                IOperationResult<UserDTO> result;
                result = new OperationResult<UserDTO>(
                    HttpStatusCode.OK,
                    $"El usuario \"{user.Identification}\" fue creado",
                    user,
                    "");
                return await Task.FromResult<IActionResult>(StatusCode(result));
            }
            catch (Exception ex)
            {
                int i = 0;
                if (ex.Message == "Ya existe un usuario con esa identificación")
                {
                    return StatusCode(
                    StatusCodes.Status406NotAcceptable,
                    new { Message = "Ya existe un usuario con la misma identificacion" }
                    );
                }
                return StatusCode(
                    StatusCodes.Status500InternalServerError
                    );
            }
            //public async Task<ActionResult>
            //return Ok("");
        }*/
        /*
         * Como no hacer un punto de API
        /// <summary>
        /// Crea usuarios de tipo Visitante
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ActionResult> Create([FromBody] VisitanteUserRequest request)
        {
            UserRequest userRequest = new UserRequest();
            userRequest.Identification = request.Identification;
            userRequest.UrlFoto = request.UrlFoto;
            userRequest.Email = request.Email;
            userRequest.Password = request.Password;
            userRequest.role = "visitante";
            userRequest.nombre = request.nombre;
            userRequest.apellido = request.apellido;
            userRequest.cedula = request.cedula;
            userRequest.nacimiento = request.nacimiento;

            UserDTO user = _userAppService.Create(userRequest);
            return Ok(_userAppService.GetAll(new UserQuery()));
        }
        */
    }
    //Por tiempo este codigo cayo aqui y aqui se queda hasta proximo desarrollo
    public interface IOperationResult
    {
        string? Error { get; set; }
        string Message { get; set; }
        HttpStatusCode StatusCode { get; set; }
        bool Success { get; }

    }

    public interface IOperationResult<T> : IOperationResult
    {
        T? Result { get; }
    }

    public interface IOperationResultList<T> : IOperationResult
    {
        IEnumerable<T>? Result { get; }
    }


    public class OperationResult : IOperationResult, IEquatable<OperationResult?>
    {
        public OperationResult(Exception ex)
        {
            Message = ex.InnerException?.InnerException?.Message ?? ex.InnerException?.Message ?? ex.Message;
            StatusCode = HttpStatusCode.InternalServerError;
#if DEBUG
            Error = ex.ToString();
#else
            Error = $"{ex.Message} {ex?.InnerException?.Message} {ex?.InnerException?.InnerException?.Message}";
#endif
        }

        public OperationResult(HttpStatusCode status, string? message = default, string? exception = null)
        {
            StatusCode = status;
            Message = message ?? $"The server responded with status code: {(int)status} {status}";
            Error = exception;
        }

        public string? Error { get; set; }
        public string? Message { get; set; } = "OK";
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public bool Success => StatusCode < HttpStatusCode.BadRequest;

        public override bool Equals(object? obj)
        {
            return Equals(obj as OperationResult);
        }

        public bool Equals(OperationResult? other)
        {
            return other is not null &&
                   Error == other.Error &&
                   Message == other.Message &&
                   StatusCode == other.StatusCode &&
                   Success == other.Success;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Error, Message, StatusCode, Success);
        }

        public override string ToString() =>
            $"{(Success ? "SUCCESS" : "ERROR")} | {(int)StatusCode} {StatusCode} : {Message} - {Error}";

        public static bool operator ==(OperationResult? left, OperationResult? right)
        {
            return EqualityComparer<OperationResult>.Default.Equals(left, right);
        }

        public static bool operator !=(OperationResult? left, OperationResult? right)
        {
            return !(left == right);
        }
    }

    public class OperationResult<T> : OperationResult, IOperationResult<T>, IEquatable<OperationResult<T>?>
    {
        public T? Result { get; internal set; }

        public OperationResult(Exception ex, T? result = default) : base(ex)
        {
            Result = result;
        }

        public OperationResult(HttpStatusCode status, string? message = null, T? result = default, string? error = default)
            : base(status, message, error)
        {
            if (result == null && StatusCode == HttpStatusCode.OK)
            {
                StatusCode = HttpStatusCode.NotFound;
                Message = message ?? "The requested record does not exist or has already been deleted!";
            }
            else
            {
                Result = result;
            }
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as OperationResult<T>);
        }

        public bool Equals(OperationResult<T>? other)
        {
            return other is not null &&
                   base.Equals(other) &&
                   Error == other.Error &&
                   Message == other.Message &&
                   StatusCode == other.StatusCode &&
                   Success == other.Success &&
                   EqualityComparer<T?>.Default.Equals(Result, other.Result);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Error, Message, StatusCode, Success, Result);
        }

        public static bool operator ==(OperationResult<T>? left, OperationResult<T>? right)
        {
            return EqualityComparer<OperationResult<T>>.Default.Equals(left, right);
        }

        public static bool operator !=(OperationResult<T>? left, OperationResult<T>? right)
        {
            return !(left == right);
        }
    }

    public class OperationResultList<T> : OperationResult, IOperationResultList<T>, IEquatable<OperationResultList<T>?>
    {
        private const int maxPageSize = 50;
        private int _pageSize = 10;
        private int _length = 0;

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > maxPageSize ? maxPageSize : value;
        }

        public int Length
        {
            get => _length;
            set => _length = value < Result.Count() ? Result.Count() : value;
        }

        public int TotalPages => Length > 0 ? (int)Math.Ceiling(Length / (double)PageSize) : 0;
        public bool HasPrevious => PageNumber > 1;
        public bool HasNext => PageNumber < TotalPages;

        public IEnumerable<T> Result { get; set; } = Enumerable.Empty<T>();

        public OperationResultList(Exception ex, IEnumerable<T>? entity = default) : base(ex)
        {
            Result = entity ?? Enumerable.Empty<T>();
        }

        public OperationResultList(HttpStatusCode status, string? message = null, IEnumerable<T>? result = default, int Offset = 1, int? Take = default, int? count = default)
            : base(status, message)
        {
            Result = result ?? Enumerable.Empty<T>();

            Length = count ?? Result.Count();
            PageSize = Take ?? Length;
            PageNumber = Offset;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as OperationResultList<T>);
        }

        public bool Equals(OperationResultList<T>? other)
        {
            return other is not null &&
                   base.Equals(other) &&
                   Error == other.Error &&
                   Message == other.Message &&
                   StatusCode == other.StatusCode &&
                   Success == other.Success &&
                   _pageSize == other._pageSize &&
                   _length == other._length &&
                   PageNumber == other.PageNumber &&
                   PageSize == other.PageSize &&
                   Length == other.Length &&
                   TotalPages == other.TotalPages &&
                   HasPrevious == other.HasPrevious &&
                   HasNext == other.HasNext &&
                   EqualityComparer<IEnumerable<T>>.Default.Equals(Result, other.Result);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(base.GetHashCode());
            hash.Add(Error);
            hash.Add(Message);
            hash.Add(StatusCode);
            hash.Add(Success);
            hash.Add(_pageSize);
            hash.Add(_length);
            hash.Add(PageNumber);
            hash.Add(PageSize);
            hash.Add(Length);
            hash.Add(TotalPages);
            hash.Add(HasPrevious);
            hash.Add(HasNext);
            hash.Add(Result);
            return hash.ToHashCode();
        }

        public static bool operator ==(OperationResultList<T>? left, OperationResultList<T>? right)
        {
            return EqualityComparer<OperationResultList<T>>.Default.Equals(left, right);
        }

        public static bool operator !=(OperationResultList<T>? left, OperationResultList<T>? right)
        {
            return !(left == right);
        }
    }
}