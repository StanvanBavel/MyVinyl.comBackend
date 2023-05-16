using EasyNetQ;
using static MyVinyl.com_rabbitmq.BackgroundServices.UserRequest;

namespace MyVinyl.com_rabbitmq.BackgroundServices
{

    public class UserRequest
    {
        public long Id { get; set; }
        public UserRequest(long Id)
        {
            Id = Id;
        }

        public class UserResponse
        {
            public string Name { get; set; }
            public UserResponse() { }

        }
    }
    public class UserEventHandler : BackgroundService
    {
        private readonly IBus _bus;

        public UserEventHandler(IBus bus)
        {
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _bus.Rpc.RespondAsync<UserRequest, UserResponse>(ProcessUserRequest);
        }

        private UserResponse ProcessUserRequest(UserRequest userRequest)
        {
            return new UserResponse() { Name = "Samson"};
        }
    }
}
