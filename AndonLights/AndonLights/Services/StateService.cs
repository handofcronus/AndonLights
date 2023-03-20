using AndonLights.DAL.Interfaces;


namespace AndonLights.Services;

public class StateService
{
    private IStateRepo _stateRepo;

    public StateService(IStateRepo stateServiceRepo)
    {
        _stateRepo = stateServiceRepo;
    }
}
