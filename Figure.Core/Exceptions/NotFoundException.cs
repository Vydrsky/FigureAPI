namespace Figure.Application.Exceptions;

public class NotFoundException : Exception {
	public override string Message { get; }

	public NotFoundException(string message) {
		Message = message;
	}
}
