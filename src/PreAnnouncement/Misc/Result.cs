using System;
using System.Collections.Generic;
using System.Text;

namespace PreAnnouncement.Misc
{

    public class Result
    {
        public bool IsSuccess => Error == null;

        public Exception Error { get; private set; }

        private Result()
        {
        }

        private Result(Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            Error = exception;
        }

        public static Result Success() => new Result();
        public static Result Fail(Exception exception) => new Result(exception);
        public static Result<T> Success<T>(T value) => Result<T>.Success(value);
    }

    public class Result<T>
    {
        public T Value
        {
            get
            {
                if (_iHaveBeenChecked == false)
                {
                    if (Error != null)
                        throw new InvalidOperationException($"You cannot access {nameof(Value)} without checking {nameof(IsSuccess)}.", Error);
                    throw new InvalidOperationException($"You cannot access {nameof(Value)} without checking {nameof(IsSuccess)}.");
                }

                return _value;
            }
        }

        public bool IsSuccess
        {
            get
            {
                _iHaveBeenChecked = true;
                return Error == null;
            }
        }

        public Exception Error { get; private set; }
        private readonly T _value;
        private bool _iHaveBeenChecked;

        private Result(T value)
        {
            _value = value;
        }

        private Result(Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            Error = exception;
        }

        public static Result<T> Success(T value) => new Result<T>(value);
        public static Result<T> Fail(Exception exception) => new Result<T>(exception);
    }
}
