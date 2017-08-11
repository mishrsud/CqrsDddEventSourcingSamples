using System;
using System.Threading.Tasks;

namespace Domain
{
    public class CreateFooCommandHandler : ICommandHandler<CreateFooCommand>
    {
        public async Task Handle(CreateFooCommand message)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1000));
            Console.WriteLine("Ta di da.. boom boom");
            Console.WriteLine("Command executed");
        }
    }
}
