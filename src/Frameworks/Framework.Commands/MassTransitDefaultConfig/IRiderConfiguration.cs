using MassTransit;

namespace Framework.Commands.MassTransitDefaultConfig;

public interface IRiderConfiguration
{
    void Configure(IRiderRegistrationConfigurator obj);
}