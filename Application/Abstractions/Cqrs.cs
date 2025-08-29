namespace OrderManagementApp.Application.Abstractions
{
    public class Cqrs
    {
        public interface ICommand { }
        public interface ICommandHandler<TCommand> where TCommand : ICommand { Task HandleAsync(TCommand command, CancellationToken ct = default); }


        public interface IQuery<TResult> { }
        public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult> { Task<TResult> HandleAsync(TQuery query, CancellationToken ct = default); }

        
    }
}
