using System;

internal class Complex
{
    public double Real { get; set; }
    public double Imaginary { get; set; }

    public Complex(double real, double imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    public Complex Add(Complex other)
    {
        return new Complex(Real + other.Real, Imaginary + other.Imaginary);
    }

    public Complex AddThrow(Complex other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        return new Complex(checked(Real + other.Real), checked(Imaginary + other.Imaginary));
    }

    public Complex AddWithCustomException(Complex other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        try
        {
            return new Complex(checked(Real + other.Real), checked(Imaginary + other.Imaginary));
        }
        catch (OverflowException ex)
        {
            throw new ComplexOperationException("Addition resulted in overflow", ex, this, other);
        }
    }

    public Complex Subtract(Complex other)
    {
        return new Complex(Real - other.Real, Imaginary - other.Imaginary);
    }

    public Complex SubtractThrow(Complex other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        return new Complex(checked(Real - other.Real), checked(Imaginary - other.Imaginary));
    }

    public Complex SubtractWithCustomException(Complex other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        try
        {
            return new Complex(checked(Real - other.Real), checked(Imaginary - other.Imaginary));
        }
        catch (OverflowException ex)
        {
            throw new ComplexOperationException("Subtraction resulted in overflow", ex, this, other);
        }
    }

    public Complex Multiply(Complex other)
    {
        return new Complex(Real * other.Real - Imaginary * other.Imaginary,
                           Real * other.Imaginary + Imaginary * other.Real);
    }

    public Complex MultiplyThrow(Complex other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        double real = checked(Real * other.Real - Imaginary * other.Imaginary);
        double imaginary = checked(Real * other.Imaginary + Imaginary * other.Real);
        return new Complex(real, imaginary);
    }

    public Complex MultiplyWithCustomException(Complex other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        try
        {
            double real = checked(Real * other.Real - Imaginary * other.Imaginary);
            double imaginary = checked(Real * other.Imaginary + Imaginary * other.Real);
            return new Complex(real, imaginary);
        }
        catch (OverflowException ex)
        {
            throw new ComplexOperationException("Multiplication resulted in overflow", ex, this, other);
        }
    }

    public Complex Divide(Complex other)
    {
        double denominator = other.Real * other.Real + other.Imaginary * other.Imaginary;
        double real = (Real * other.Real + Imaginary * other.Imaginary) / denominator;
        double imaginary = (Imaginary * other.Real - Real * other.Imaginary) / denominator;
        return new Complex(real, imaginary);
    }

    public Complex DivideThrow(Complex other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        double denominator = other.Real * other.Real + other.Imaginary * other.Imaginary;

        if (denominator == 0)
        {
            throw new DivideByZeroException("Division by zero");
        }

        double real = checked((Real * other.Real + Imaginary * other.Imaginary) / denominator);
        double imaginary = checked((Imaginary * other.Real - Real * other.Imaginary) / denominator);
        return new Complex(real, imaginary);
    }

    public Complex DivideWithCustomException(Complex other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        double denominator = other.Real * other.Real + other.Imaginary * other.Imaginary;

        if (denominator == 0)
        {
            throw new ComplexOperationException("Division by zero", this, other);
        }

        try
        {
            double real = checked((Real * other.Real + Imaginary * other.Imaginary) / denominator);
            double imaginary = checked((Imaginary * other.Real - Real * other.Imaginary) / denominator);
            return new Complex(real, imaginary);
        }
        catch (OverflowException ex)
        {
            throw new ComplexOperationException("Division resulted in overflow", ex, this, other);
        }
    }

    public Complex Conjugate()
    {
        return new Complex(Real, -Imaginary);
    }

    public class ComplexOperationException : Exception
    {
        public Complex FirstOperand { get; }
        public Complex SecondOperand { get; }

        public ComplexOperationException(string message, Complex firstOperand, Complex secondOperand)
            : base(message)
        {
            FirstOperand = firstOperand;
            SecondOperand = secondOperand;
        }

        public ComplexOperationException(string message, Exception innerException, Complex firstOperand, Complex secondOperand)
            : base(message, innerException)
        {
            FirstOperand = firstOperand;
            SecondOperand = secondOperand;
        }
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Complex other = (Complex)obj;
        return Real == other.Real && Imaginary == other.Imaginary;
    }

    public override int GetHashCode()
    {
        return Real.GetHashCode() ^ Imaginary.GetHashCode();
    }

    public override string ToString()
    {
        return $"({Real}, {Imaginary})";
    }
}