Public Class Generadores
    Public Sub New()

    End Sub

    Public Function Exponencial(med As Double, n As Integer)
        Dim lista(0 To (n - 1)) As Double
        Dim lambda As Double = 1 / med
        Dim yvalue As Double
        For num As Decimal = 0 To (n - 1)
            yvalue = ((-1) / lambda) * (Math.Log(1 - Rnd()))
            yvalue = Math.Round(yvalue, 3)
            lista(num) = yvalue
        Next
        Return lista
    End Function
    Public Function Uniforme(rangoInferior As Integer, rangoSuperior As Integer, tamañoMuestra As Integer, cantidadIntervalos As Integer)
        Dim lista(0 To (tamañoMuestra - 1)) As Double
        Dim yvalue As Double
        For num As Decimal = 0 To (tamañoMuestra - 1)
            yvalue = (rangoInferior + (rangoSuperior - rangoInferior) * Rnd())
            yvalue = Math.Round(yvalue, 3)
            lista(num) = yvalue
        Next
        Return lista
    End Function
    Public Function Poisson(lambda As Double, n As Integer)
        Dim lista(0 To (n - 1)) As Double

        Dim p As Double
        Dim yvalue As Integer
        Dim a As Double = Math.Exp(-lambda)
        Dim u As Double


        For num As Decimal = 0 To (n - 1)
            p = 1
            yvalue = 0
            Do While (p >= a)
                u = Rnd()
                p = p * u
                yvalue = yvalue + 1
            Loop
            lista(num) = yvalue
        Next
        Return lista
    End Function

    Public Function Normal(media As Double, n As Integer, desviacion As Double)

        Dim pi As Double = Math.PI
        Dim lista(0 To (n - 1)) As Double
        Dim yvalue As Double

        For num As Decimal = 0 To (n - 1)
            Dim aux1 As Double = Rnd()
            Dim aux2 As Double = Rnd()
            Dim z As Double
            z = Math.Sqrt(-2 * Math.Log(aux1)) * (Math.Sin(2 * pi * aux2))
            yvalue = media + desviacion * (z)
            lista(num) = Math.Round(yvalue, 4)
        Next
        Return lista

    End Function
End Class
