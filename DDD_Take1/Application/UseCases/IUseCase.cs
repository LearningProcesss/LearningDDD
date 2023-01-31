public interface IUseCase<in Tin, Tout>
{
    Task<Tout> Handle(Tin input);
}