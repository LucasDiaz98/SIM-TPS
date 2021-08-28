Imports System.Windows.Forms.DataVisualization.Charting
Public Class Principal
    Dim var As Boolean

    ''lista tabulada de xi cuadrado para 0.95 de confianza
    Public listaChipcuadrada() As String = {"3,84", "5,99", "7,81", "9,49", "11,14", "12,6", "14,1", "15,5", "16,9", "18,3", "19,7", "21,0", "22,4", "23,7", "25,0", "26.3", "26,3",
                             "27,6", "28,9", "30,1", "31,4", "32,7", "33.9", "35,2", "36,4", "37,7", "38,9", "40,1", "41,3", "42,6", "43,8", "", "", "", "", "", "", "", "", "", "55,8",
                             "", "", "", "", "", "", "", "", "", "67,5", "", "", "", "", "", "", "", "", "", "79,1", "", "", "", "", "", "", "", "", "", "90,5", "", "", "", "", "", "",
                             "", "", "", "101,9", "", "", "", "", "", "", "", "", "", "113,1", "", "", "", "", "", "", "", "", "", "124,3"}

    Private Sub Principal_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Location = Screen.PrimaryScreen.WorkingArea.Location  ''ajusto el form 
        Me.Size = Screen.PrimaryScreen.WorkingArea.Size           '' para que se expanda
        Me.TabControl1.Size = Screen.PrimaryScreen.WorkingArea.Size


    End Sub

    Private Sub TabPage1_Click(sender As System.Object, e As System.EventArgs) Handles TabPage1.Click
        ChartUniforme.Series(0).Points.Clear()

    End Sub

    Private Sub graficarBTN_Click(sender As System.Object, e As System.EventArgs) Handles graficarBTN.Click
        If validar() Then
            If var = False Then
                MsgBox("Por favor ingrese todos los valores solicitados")
            Else
                Dim eje_y As Double
                Dim random As New Random()
                Dim intervalos As Double
                Dim n As Integer = Ntxt.Text
                Dim lista(0 To (n - 1)) As Double
                Dim count As Integer = 0
                Dim sum As Integer
                Dim k As Integer = Ktxt.Text ''numero de intervalos equiprobables entre 0 y 1
                Dim med As Double
                Dim xCuadradoTabulado As Double = 0 ''variable que asumira el rol de xi cuadrado tabulado
                Dim xCuadradoCalculado As Double = 0 ''variable que asumira el rol de xi cuadrado para ser comparado con la tabla
                Dim rangoInferior As Integer = Convert.ToInt32(txtA.Text.Trim)
                Dim rangoSuperior As Integer = Convert.ToInt32(txtB.Text.Trim)


                'For num As Decimal = 0 To (Ntxt.Text - 1) ''iteramos la cantidad de n-1 elegidas por el usuario
                '    eje_y = Rnd() ''asignamos numero random al eje y
                '    lst_numeros.Items.Add(Math.Round(eje_y, 5)) ''lo agregamos a la lista q se muestra en pantalla
                '    'Round(yvalue, 3)

                '    lista(num) = eje_y ''lo agregamos a la lista para el lbl

                'Nex
                Dim metodos As New Generadores
                lista = metodos.Uniforme(rangoInferior, rangoSuperior, n, k)

                Dim max = lista(0)
                Dim min = lista(0)
                For count = 0 To (n - 1) ''imprimimos la lista en el lbl
                    lst_numeros.Items.Add(Math.Round(lista(count), 5))
                    If lista(count) > max Then ''buscamos el maximo de la lista para determinar los intervalos y el largo del grafico
                        max = lista(count)
                    End If
                    If lista(count) < min Then ''buscamos el minimo de la lista para determinar los intervalos y el largo del grafico
                        min = lista(count)
                    End If
                Next count



                intervalos = (rangoSuperior - rangoInferior) / k ''probabilidad de cada intevalo
                med = n / k ''MEDIDA DE LOS INTERVALOS'' = frecuencia esperada

                Dim fila As Double = 0
                intervalos = Math.Round(intervalos, 3)
                ''ESTABLECE EL VALOR REDONDEADO DE LOS INTERVALOS EN 3 DIGITOS''
                ''**************************************************************************
                Dim fe As Double = 0
                Dim fo As Double = 0
                Dim xiCalculado As Double = 0
                Dim contadorLabel As Integer = 0
                Dim filaXiCuadrado As Integer = 0
                Dim acumuladorFila As Integer = 0
                Dim vectorUltimaFila(0 To (4)) As Double

                ''**************************************************************************
                For i As Double = rangoInferior To (rangoSuperior - 1) Step intervalos ''recorremos los intervalos saltando de forma entera, pero el intervalo es decimal.
                    ''de esta forma buscamos los numeros en la lista que pertenecen a cada intervalo para sumarlos
                    ''a la muestra
                    sum = 0 ''contador de frecuencia , que sera al final el ciclo la frecuencia observada de dicho valor
                    For count = 0 To (n - 1) ''iteramos desde 0 hasta la cantidad de muestras solicitadas
                        If i <= lista(count) Then ''si el numero pseudo-aleaotrio es mayor que el limite inferior del intervalo 
                            If lista(count) < (i + intervalos) Then ''y menor que el limite superior del intervalo
                                sum = sum + 1 ''incremento de la frecuencia de aparicion en dicho intervalo
                            End If
                        End If
                    Next count
                    ChartUniforme.Series("Observado").Points.AddXY((i + intervalos), sum) ''graficamos el valor observado en el intervalo i
                    ChartUniforme.Series("Esperado").Points.AddXY((i + intervalos), med) '' y graficamos el valor del esperado en el intervalo i
                    '' calculo de chi - cuadrado
                    dtgvChiCuadrado.Rows.Add() ''agregamos una fila por cada ciclo
                    dtgvChiCuadrado.Rows((fila)).Cells(0).Value = i.ToString() + " - " + (i + intervalos).ToString ''agregamos el rango del intervalo a la grilla
                    dtgvChiCuadrado.Rows((fila)).Cells(1).Value = sum.ToString ''agregamos la frecuencia observada
                    dtgvChiCuadrado.Rows((fila)).Cells(2).Value = med.ToString ''agregamos frecuencia esperada

                    Dim fdif As Double = sum - med
                    dtgvChiCuadrado.Rows((fila)).Cells(3).Value = fdif.ToString ''agregamos diferencia de frecuencias
                    Dim difCuadrado As Double = fdif * fdif
                    dtgvChiCuadrado.Rows((fila)).Cells(4).Value = difCuadrado.ToString ''agregamos diferencia al cuadrado
                    Dim xCuadradoI As Double = difCuadrado / med ''calculo ((fo - fe)^2 )/ fe
                    dtgvChiCuadrado.Rows((fila)).Cells(5).Value = xCuadradoI.ToString ''agreagamos xicuadrado

                    xCuadradoCalculado += xCuadradoI ''voy realizando sumatoria de ((fo - fe)^2 )/ fe
                    ''*********************************************************************************

                    fe += med
                    fo += sum
                    acumuladorFila += 1
                    If fe >= 5 Then ''entra si la frecuencia observada supera 5 o si 
                        vectorUltimaFila(4) = xiCalculado
                        xiCalculado += ((fo - fe) ^ 2) / fe ''seteo los valores de las frecuencias que acumule y calculo el xiCalculado

                        dtgXiUniforme.Rows.Add()
                        dtgXiUniforme.Rows(filaXiCuadrado).Cells(0).Value = Math.Round(((i - (acumuladorFila - 1) * intervalos)), 4).ToString + " - " + Math.Round((i + intervalos), 4).ToString
                        vectorUltimaFila(0) = Math.Round((i - (acumuladorFila - 1) * intervalos), 4) ''guardo el rango inferior del intervalo para usarlo posiblemente en futuro
                        vectorUltimaFila(1) = fo
                        vectorUltimaFila(2) = fe
                        dtgXiUniforme.Rows(filaXiCuadrado).Cells(1).Value = fo
                        dtgXiUniforme.Rows(filaXiCuadrado).Cells(2).Value = fe
                        dtgXiUniforme.Rows(filaXiCuadrado).Cells(3).Value = Math.Round(xiCalculado, 4)
                        acumuladorFila = 0
                        fo = 0
                        fe = 0
                        filaXiCuadrado += 1
                    End If
                    vectorUltimaFila(3) = Math.Round(i + intervalos, 4) ''mantengo siempre el ultimo rango


                    ''**********************************************************************************
                    fila += 1
                Next
                'dtgvChiCuadrado.Rows.Add()
                'dtgvChiCuadrado.Rows((fila)).Cells(4).Value = "Xi Cuadrado caluculado"
                'dtgvChiCuadrado.Rows((fila)).Cells(5).Value = xCuadradoCalculado.ToString ''agregamos el xi cuadrado a la grilla
                ''**********************************************
                ''termino de calcular los datos de xiCuadrado
                dtgXiUniforme.Rows.Add()
                'lblVNormal.Text = ("los grados de libertad son: " + (kIntervalos - 2).ToString).ToString
                Dim xiTabulado As String = listaChipcuadrada(k - 2)

                dtgXiUniforme.Rows(filaXiCuadrado).Cells(2).Value = "Xi Tabulado"
                If xiTabulado Is "" Then ''si la posicion del vector no tiene valores, cambia el string xiTabulado
                    dtgXiUniforme.Rows(filaXiCuadrado).Cells(3).Value = "no hay valores"
                Else
                    dtgXiUniforme.Rows(filaXiCuadrado).Cells(3).Value = xiTabulado.ToString
                    dtgXiUniforme.Rows.Add()
                    filaXiCuadrado += 1
                    dtgXiUniforme.Rows((filaXiCuadrado)).Cells(2).Value = "Xi Calculado"
                    dtgXiUniforme.Rows((filaXiCuadrado)).Cells(3).Value = xiCalculado.ToString
                    dtgXiUniforme.Rows.Add()
                    filaXiCuadrado += 1
                    If Convert.ToDouble(xiTabulado.ToString) > xiCalculado Then
                        dtgXiUniforme.Rows((filaXiCuadrado)).Cells(3).Value = "Aceptado"
                        dtgXiUniforme.Rows((filaXiCuadrado)).Cells(3).Style.BackColor = Color.Green
                    Else
                        dtgXiUniforme.Rows((filaXiCuadrado)).Cells(3).Value = "Rechazado"
                        dtgXiUniforme.Rows((filaXiCuadrado)).Cells(3).Style.BackColor = Color.Red
                    End If
                End If


                ''****************************************************

                ChartUniforme.Series("Observado").ChartType = SeriesChartType.Column
                ChartUniforme.Series("Observado").XValueType = ChartValueType.Double
                ChartUniforme.Series("Observado").YValueType = ChartValueType.Int32
                ChartUniforme.ChartAreas(0).AxisX.Interval = intervalos
                ChartUniforme.Series("Observado").IsVisibleInLegend = True

                lblV.Text = "los grados de libertado son: " + (k - 1).ToString
                'dtgXiUniforme.Rows.Add()
                'dtgXiUniforme.Rows((fila + 1)).Cells(4).Value = "Xi cudrado Tabulado"
                'Dim xiTabulado As String = listaChipcuadrada(k - 2).ToString()
                'If xiTabulado Is "" Then ''si la posicion del vector no tiene valores, cambia el string xiTabulado
                '    xiTabulado = "no hay valores para esos grados de libertad"
                '    dtgXiUniforme.Rows((fila + 1)).Cells(5).Value = xiTabulado.ToString
                'Else
                '    dtgXiUniforme.Rows((fila + 1)).Cells(5).Value = xiTabulado.ToString
                '    If Convert.ToDouble(xiTabulado.ToString) > xCuadradoCalculado Then
                '        dtgXiUniforme.Rows((fila + 1)).Cells(3).Value = "Aceptado"
                '        dtgXiUniforme.Rows((fila + 1)).Cells(3).Style.BackColor = Color.Green
                '    Else
                '        dtgXiUniforme.Rows((fila + 1)).Cells(3).Value = "Rechazado"
                '        dtgXiUniforme.Rows((fila + 1)).Cells(3).Style.BackColor = Color.Red
                '    End If
                'End If
            End If
        End If
    End Sub

    Private Function validar() As Boolean
        If (Ntxt.Text = "" Or Ktxt.Text = "") Then
            var = False
        ElseIf (txtA.Text = "" Or txtB.Text = "") Then
            var = False
        ElseIf Convert.ToInt32(txtA.Text.Trim) >= Convert.ToInt32(txtB.Text.Trim) Then
            var = False
        Else
            var = True
        End If


        Return var
    End Function
    Private Sub Ntxt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Ntxt.KeyPress
        If Char.IsNumber(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        End If
    End Sub

    Private Sub Ktxt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Ktxt.KeyPress
        If Char.IsNumber(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        End If
    End Sub

    Private Sub btnLimpiar_Click(sender As System.Object, e As System.EventArgs) Handles btnLimpiar.Click
        ChartUniforme.Series(0).Points.Clear()
        ChartUniforme.Series(1).Points.Clear()
        Ntxt.Text = ""
        Ktxt.Text = ""
        lst_numeros.Items.Clear()
        lst_numeros.Show()
        dtgvChiCuadrado.Rows.Clear()
        txtA.Text = ""
        txtB.Text = ""


    End Sub

    Private Sub TabPage2_Click(sender As System.Object, e As System.EventArgs) Handles TabPage2.Click

    End Sub

    Private Sub btngenerarNormal_Click(sender As System.Object, e As System.EventArgs) Handles btngenerarNormal.Click

        dtgNormal.Rows.Clear() ''limpio la tabla
        dtgXiCuadradoNormal.Rows.Clear()
        ChartNormal.Series(0).Points.Clear()

        lstbNumeros.Items.Clear()

        Dim media As Double = Convert.ToDouble(txtMedia.Text.Trim)
        Dim muestra As Integer = txtTamañoMuestra.Text
        Dim desviacion As Double = Convert.ToDouble(txtDesviacion.Text.Trim)

        Dim kIntervalos As Integer = txtIntervalos.Text
        Dim generadores As Generadores = New Generadores()
        Dim lista(0 To (muestra - 1)) As Double

        lista = generadores.Normal(media, muestra, desviacion)
        Dim sum As Integer

        Dim max = lista(0)
        Dim min = lista(0)
        For count = 0 To (muestra - 1) ''imprimimos la lista en el lbl
            lstbNumeros.Items.Add(Math.Round(lista(count), 5))
            If lista(count) > max Then ''buscamos el maximo de la lista para determinar los intervalos y el largo del grafico
                max = lista(count)
            End If
            If lista(count) < min Then ''buscamos el minimo de la lista para determinar los intervalos y el largo del grafico
                min = lista(count)
            End If
        Next count

        Dim intervalo As Double = (max - min) / kIntervalos ''determino el ancho de los intervalos
        Dim fila As Integer = 0
        Dim fe As Double = 0
        Dim fo As Double = 0
        Dim xiCalculado As Double = 0
        Dim contadorLabel As Integer = 0
        Dim filaXiCuadrado As Integer = 0
        Dim acumuladorFila As Integer = 0
        Dim vectorUltimaFila(0 To (4)) As Double

        For i As Double = min To (max - intervalo) Step intervalo
            sum = 0
            For count = 0 To (muestra - 1)
                If i <= lista(count) Then
                    If lista(count) < (i + intervalo) Then
                        sum = sum + 1
                    End If
                End If
            Next count

            Dim x = Math.Round(i + intervalo, 2) ''se redondea el ancho del intervalo


            ChartNormal.Series("Observado").Points.AddXY((i + (intervalo / 2)), sum)

            ChartNormal.Series("Observado").Points(Math.Round(contadorLabel, 1)).Label = sum.ToString + vbCrLf + "[ " + Math.Round(i, 2).ToString + " - " + Math.Round(x, 2).ToString + " ]"

            contadorLabel += 1
            ''calculo Z
            Dim z = ((i + (intervalo / 2)))
            ''- media) / desviacion

            Dim px = (1 / (1 * Math.Sqrt((2 * (Math.PI))))) * Math.Exp(-0.5 * ((z) ^ 2))
            ''agrego fila
            dtgNormal.Rows.Add()
            ''cargo valores en las fila i , y las celdas respectivas

            dtgNormal.Rows((fila)).Cells(0).Value = Math.Round(z, 4).ToString
            dtgNormal.Rows((fila)).Cells(1).Value = Math.Round(i, 4).ToString + " - " + Math.Round((i + intervalo), 4).ToString
            dtgNormal.Rows((fila)).Cells(2).Value = Math.Round(sum, 4).ToString
            dtgNormal.Rows((fila)).Cells(3).Value = Math.Round(px, 4).ToString
            dtgNormal.Rows((fila)).Cells(4).Value = Math.Round((px * muestra), 4).ToString
            fe += (px * muestra)
            fo += sum
            acumuladorFila += 1
            If fe >= 5 Then ''entra si la frecuencia observada supera 5 o si 
                vectorUltimaFila(4) = xiCalculado
                xiCalculado += ((fo - fe) ^ 2) / fe ''seteo los valores de las frecuencias que acumule y calculo el xiCalculado

                dtgXiCuadradoNormal.Rows.Add()
                dtgXiCuadradoNormal.Rows(filaXiCuadrado).Cells(0).Value = Math.Round((i - (acumuladorFila - 1 * intervalo)), 4).ToString + " - " + Math.Round((i + intervalo), 4).ToString
                vectorUltimaFila(0) = Math.Round((i - (acumuladorFila - 1) * intervalo), 4) ''guardo el rango inferior del intervalo para usarlo posiblemente en futuro
                vectorUltimaFila(1) = fo
                vectorUltimaFila(2) = fe
                dtgXiCuadradoNormal.Rows(filaXiCuadrado).Cells(1).Value = fo
                dtgXiCuadradoNormal.Rows(filaXiCuadrado).Cells(2).Value = fe
                dtgXiCuadradoNormal.Rows(filaXiCuadrado).Cells(3).Value = Math.Round(xiCalculado, 4)
                acumuladorFila = 0
                fo = 0
                fe = 0
                filaXiCuadrado += 1
            End If

            fila += 1
            vectorUltimaFila(3) = Math.Round(i + intervalo, 4) ''mantengo siempre el ultimo rango
        Next

        ''si habia mas intervalos por agrupar recalculo
        If acumuladorFila > 0 Then

            dtgXiCuadradoNormal.Rows(filaXiCuadrado - 1).Cells(0).Value = vectorUltimaFila(0).ToString + " - " + vectorUltimaFila(3).ToString
            fo += vectorUltimaFila(1)
            fe += vectorUltimaFila(2)
            dtgXiCuadradoNormal.Rows(filaXiCuadrado - 1).Cells(1).Value = fo
            dtgXiCuadradoNormal.Rows(filaXiCuadrado - 1).Cells(2).Value = fe
            xiCalculado = vectorUltimaFila(4) + (((fo - fe) ^ 2) / fe)
            dtgXiCuadradoNormal.Rows(filaXiCuadrado - 1).Cells(3).Value = Math.Round((((fo - fe) ^ 2) / fe), 4)

        End If
        ''termino de calcular los datos de xiCuadrado
        dtgXiCuadradoNormal.Rows.Add()
        lblVNormal.Text = ("los grados de libertad son: " + (kIntervalos - 2).ToString).ToString
        Dim xiTabulado As String = listaChipcuadrada(kIntervalos - 3)

        dtgXiCuadradoNormal.Rows(filaXiCuadrado).Cells(2).Value = "Xi Tabulado"
        If xiTabulado Is "" Then ''si la posicion del vector no tiene valores, cambia el string xiTabulado
            dtgXiCuadradoNormal.Rows(filaXiCuadrado).Cells(3).Value = "no hay valores"
        Else
            dtgXiCuadradoNormal.Rows(filaXiCuadrado).Cells(3).Value = xiTabulado.ToString
            dtgXiCuadradoNormal.Rows.Add()
            filaXiCuadrado += 1
            dtgXiCuadradoNormal.Rows((filaXiCuadrado)).Cells(2).Value = "Xi Calculado"
            dtgXiCuadradoNormal.Rows((filaXiCuadrado)).Cells(3).Value = xiCalculado.ToString
            dtgXiCuadradoNormal.Rows.Add()
            filaXiCuadrado += 1
            If Convert.ToDouble(xiTabulado.ToString) > xiCalculado Then
                dtgXiCuadradoNormal.Rows((filaXiCuadrado)).Cells(3).Value = "Aceptado"
                dtgXiCuadradoNormal.Rows((filaXiCuadrado)).Cells(3).Style.BackColor = Color.Green
            Else
                dtgXiCuadradoNormal.Rows((filaXiCuadrado)).Cells(3).Value = "Rechazado"
                dtgXiCuadradoNormal.Rows((filaXiCuadrado)).Cells(3).Style.BackColor = Color.Red
            End If
        End If


        ChartNormal.Series("Observado").ChartType = SeriesChartType.Column
        ChartNormal.Series("Observado").XValueType = ChartValueType.Int32
        ChartNormal.Series("Observado").YValueType = ChartValueType.Int32
        ChartNormal.ChartAreas(0).AxisX.Interval = 0.5
        ChartNormal.Series("Observado").IsValueShownAsLabel = True

    End Sub


    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        dtgNormal.Rows.Clear()
        txtDesviacion.Text = ""
        txtMedia.Text = ""
        txtIntervalos.Text = ""
        txtTamañoMuestra.Text = ""
    End Sub

    Private Sub TabPage3_Click(sender As System.Object, e As System.EventArgs) Handles TabPage3.Click


    End Sub

    Private Sub btnGenrarPoisson_Click(sender As System.Object, e As System.EventArgs) Handles btnGenrarPoisson.Click

        dtgPoisson.Rows.Clear() ''limpio la tabla
        dtgXiPoisson.Rows.Clear()
        ChartPoisson.Series(0).Points.Clear()

        lstbPoisson.Items.Clear()

        Dim lambdaPoisson As Double = txtlambda.Text.Trim
        Dim tamañoMuestraPoisson As Integer = txtTamañoPoisson.Text.Trim
        Dim intervalos As Integer = txtIntervalosPoisson.Text.Trim

        Dim metodos As New Generadores
        Dim lista = metodos.Poisson(lambdaPoisson, tamañoMuestraPoisson)
        Dim max = lista(0)
        Dim min = lista(0)
        For count = 0 To (tamañoMuestraPoisson - 1) ''imprimimos la lista en el lbl
            lstbPoisson.Items.Add(Math.Round(lista(count), 5))
            If lista(count) > max Then ''buscamos el maximo de la lista para determinar los intervalos y el largo del grafico
                max = lista(count)
            End If
            If lista(count) < min Then ''buscamos el minimo de la lista para determinar los intervalos y el largo del grafico
                min = lista(count)
            End If
        Next count
        Dim intervalo As Double = (max) / intervalos ''determino el ancho de los intervalos
        Dim sum As Integer
        Dim fila As Integer = 0

        Dim fe As Double = 0
        Dim fo As Double = 0

        Dim xiCalculado As Double = 0
        Dim contadorLabel As Integer = 0
        Dim filaXiCuadrado As Integer = 0
        Dim acumuladorFila As Integer = 0
        Dim vectorUltimaFila(0 To (4)) As Double


        For i As Double = 0 To (max - intervalo) Step intervalo
            sum = 0
            For count = 0 To (tamañoMuestraPoisson - 1)
                If i <= lista(count) Then
                    If lista(count) < (i + intervalo) Then
                        sum = sum + 1
                    End If
                End If
            Next count

            Dim x = i + (intervalo / 2) ''para poder graficar en mitad del intervalo
            x = Math.Round(x, 3) ''se trunca el ancho del numero

            'Chart1.Series("Observado").Points.AddXY(x, sum)
            ChartPoisson.Series("Observado").Points.AddXY((i + (intervalo / 2)), sum)

            ChartPoisson.Series("Observado").Points(Math.Round(contadorLabel, 1)).Label = sum.ToString + vbCrLf + "[ " + Math.Round(i, 2).ToString + " - " + Math.Round(x, 2).ToString + " ]"

            contadorLabel += 1
            ''agrego fila
            dtgPoisson.Rows.Add()
            ''calclo de la p(x) de cada intervalo
            Dim px As Double = ((lambdaPoisson ^ i) * Math.Exp(-lambdaPoisson)) / FACTORIAL(i)

            ''cargo valores en las fila i , y las celdas respectivas
            dtgPoisson.Rows((fila)).Cells(0).Value = Math.Round(i, 4).ToString + " - " + Math.Round((i + intervalo), 4).ToString
            dtgPoisson.Rows((fila)).Cells(1).Value = sum.ToString.Trim
            dtgPoisson.Rows((fila)).Cells(2).Value = px.ToString.Trim
            dtgPoisson.Rows((fila)).Cells(3).Value = (px * tamañoMuestraPoisson).ToString
            fe += (px * tamañoMuestraPoisson)
            fo += sum
            acumuladorFila += 1
            If fe >= 5 Then ''entra si la frecuencia observada supera 5 o si 
                vectorUltimaFila(4) = xiCalculado
                xiCalculado += ((fo - fe) ^ 2) / fe ''seteo los valores de las frecuencias que acumule y calculo el xiCalculado

                dtgXiPoisson.Rows.Add()
                dtgXiPoisson.Rows(filaXiCuadrado).Cells(0).Value = Math.Round(((i - (acumuladorFila - 1) * intervalo)), 4).ToString + " - " + Math.Round((i + intervalo), 4).ToString
                vectorUltimaFila(0) = Math.Round((i - (acumuladorFila - 1) * intervalo), 4) ''guardo el rango inferior del intervalo para usarlo posiblemente en futuro
                vectorUltimaFila(1) = fo
                vectorUltimaFila(2) = fe
                dtgXiPoisson.Rows(filaXiCuadrado).Cells(1).Value = fo
                dtgXiPoisson.Rows(filaXiCuadrado).Cells(2).Value = fe
                dtgXiPoisson.Rows(filaXiCuadrado).Cells(3).Value = Math.Round(xiCalculado, 4)
                acumuladorFila = 0
                fo = 0
                fe = 0
                filaXiCuadrado += 1
            End If

            fila += 1

            vectorUltimaFila(3) = Math.Round(i + intervalo, 4) ''mantengo siempre el ultimo rango
        Next

        ''si habia mas intervalos por agrupar recalculo
        If acumuladorFila > 0 Then

            dtgXiPoisson.Rows(filaXiCuadrado - 1).Cells(0).Value = vectorUltimaFila(0).ToString + " - " + vectorUltimaFila(3).ToString
            fo += vectorUltimaFila(1)
            fe += vectorUltimaFila(2)
            dtgXiPoisson.Rows(filaXiCuadrado - 1).Cells(1).Value = fo
            dtgXiPoisson.Rows(filaXiCuadrado - 1).Cells(2).Value = fe
            xiCalculado = vectorUltimaFila(4) + (((fo - fe) ^ 2) / fe)
            dtgXiPoisson.Rows(filaXiCuadrado - 1).Cells(3).Value = Math.Round((((fo - fe) ^ 2) / fe), 4)

        End If
        ''termino de calcular los datos de xiCuadrado
        dtgXiPoisson.Rows.Add()
        lblVNormal.Text = ("los grados de libertad son: " + (intervalos - 1).ToString).ToString
        Dim xiTabulado As String = listaChipcuadrada(intervalos - 2)

        dtgXiPoisson.Rows(filaXiCuadrado).Cells(2).Value = "Xi Tabulado"
        If xiTabulado Is "" Then ''si la posicion del vector no tiene valores, cambia el string xiTabulado
            dtgXiPoisson.Rows(filaXiCuadrado).Cells(3).Value = "no hay valores"
        Else
            dtgXiPoisson.Rows(filaXiCuadrado).Cells(3).Value = xiTabulado.ToString
            dtgXiPoisson.Rows.Add()
            filaXiCuadrado += 1
            dtgXiPoisson.Rows((filaXiCuadrado)).Cells(2).Value = "Xi Calculado"
            dtgXiPoisson.Rows((filaXiCuadrado)).Cells(3).Value = xiCalculado.ToString
            dtgXiPoisson.Rows.Add()
            filaXiCuadrado += 1
            If Convert.ToDouble(xiTabulado.ToString) > xiCalculado Then
                dtgXiPoisson.Rows((filaXiCuadrado)).Cells(3).Value = "Aceptado"
                dtgXiPoisson.Rows((filaXiCuadrado)).Cells(3).Style.BackColor = Color.Green
            Else
                dtgXiPoisson.Rows((filaXiCuadrado)).Cells(3).Value = "Rechazado"
                dtgXiPoisson.Rows((filaXiCuadrado)).Cells(3).Style.BackColor = Color.Red
            End If
        End If
        ChartPoisson.Series("Observado").ChartType = SeriesChartType.Column
        ChartPoisson.Series("Observado").XValueType = ChartValueType.Int32
        ChartPoisson.Series("Observado").YValueType = ChartValueType.Int32
        ChartPoisson.ChartAreas(0).AxisX.Interval = 0.5
        ChartPoisson.Series("Observado").IsValueShownAsLabel = True



    End Sub
    Function FACTORIAL(numero As Integer)
        Dim n As Integer
        FACTORIAL = 1
        For n = 1 To numero
            FACTORIAL = FACTORIAL * n
        Next
    End Function

    Private Sub btnGenerarExponencial_Click(sender As System.Object, e As System.EventArgs) Handles btnGenerarExponencial.Click
        dtgbExponencial.Rows.Clear() ''limpio la tabla
        dtgXiExponencial.Rows.Clear()
        chartExponencial.Series(0).Points.Clear()

        lstbExponencial.Items.Clear()

        Dim tamañoMuestraExponencial As Integer = txtTamañoMuestraExp.Text.Trim
        Dim intervalos As Integer = txtIntervalosExp.Text.Trim
        Dim media As Double = Convert.ToDouble(txtMediaExp.Text.Trim)
        Dim metodos As New Generadores
        Dim lista = metodos.Exponencial(media, tamañoMuestraExponencial)
        Dim max = lista(0)
        Dim min = lista(0)
        Dim lamdaExp As Double
        lamdaExp = 1 / media
        For count = 0 To (tamañoMuestraExponencial - 1) ''imprimimos la lista en el lbl
            lstbExponencial.Items.Add(Math.Round(lista(count), 5))
            If lista(count) > max Then ''buscamos el maximo de la lista para determinar los intervalos y el largo del grafico
                max = lista(count)
            End If
            If lista(count) < min Then ''buscamos el minimo de la lista para determinar los intervalos y el largo del grafico
                min = lista(count)
            End If
        Next count
        Dim intervalo As Double = (max) / intervalos ''determino el ancho de los intervalos
        Dim sum As Integer
        Dim fila As Integer = 0

        Dim fe As Double = 0
        Dim fo As Double = 0

        Dim xiCalculado As Double = 0
        Dim contadorLabel As Integer = 0
        Dim filaXiCuadrado As Integer = 0
        Dim acumuladorFila As Integer = 0
        Dim vectorUltimaFila(0 To (4)) As Double


        For i As Double = 0 To (max - intervalo) Step intervalo
            sum = 0
            For count = 0 To (tamañoMuestraExponencial - 1)
                If i <= lista(count) Then
                    If lista(count) < (i + intervalo) Then
                        sum = sum + 1
                    End If
                End If
            Next count

            Dim x = i + (intervalo / 2) ''para poder graficar en mitad del intervalo
            x = Math.Round(x, 3) ''se trunca el ancho del numero

            'Chart1.Series("Observado").Points.AddXY(x, sum)
            chartExponencial.Series("Observado").Points.AddXY((i + (intervalo / 2)), sum)

            chartExponencial.Series("Observado").Points(Math.Round(contadorLabel, 1)).Label = sum.ToString + vbCrLf + "[ " + Math.Round(i, 2).ToString + " - " + Math.Round(x, 2).ToString + " ]"

            contadorLabel += 1
            ''agrego fila
            dtgbExponencial.Rows.Add()
            ''calclo de la p(x) de cada intervalo
            Dim px As Double = (1 - Math.Exp(-(lamdaExp * (i + intervalo)))) - (1 - Math.Exp(-(lamdaExp * (i))))

            ''cargo valores en las fila i , y las celdas respectivas
            dtgbExponencial.Rows((fila)).Cells(0).Value = Math.Round(i, 4).ToString + " - " + Math.Round((i + intervalo), 4).ToString
            dtgbExponencial.Rows((fila)).Cells(1).Value = sum.ToString.Trim
            dtgbExponencial.Rows((fila)).Cells(2).Value = px.ToString.Trim
            dtgbExponencial.Rows((fila)).Cells(3).Value = (px * tamañoMuestraExponencial).ToString
            fe += (px * tamañoMuestraExponencial)
            fo += sum
            acumuladorFila += 1
            If fe >= 5 Then ''entra si la frecuencia observada supera 5 o si 
                vectorUltimaFila(4) = xiCalculado
                xiCalculado += ((fo - fe) ^ 2) / fe ''seteo los valores de las frecuencias que acumule y calculo el xiCalculado

                dtgXiExponencial.Rows.Add()
                dtgXiExponencial.Rows(filaXiCuadrado).Cells(0).Value = Math.Round(((i - (acumuladorFila - 1) * intervalo)), 4).ToString + " - " + Math.Round((i + intervalo), 4).ToString
                vectorUltimaFila(0) = Math.Round((i - (acumuladorFila - 1) * intervalo), 4) ''guardo el rango inferior del intervalo para usarlo posiblemente en futuro
                vectorUltimaFila(1) = fo
                vectorUltimaFila(2) = fe
                dtgXiExponencial.Rows(filaXiCuadrado).Cells(1).Value = fo
                dtgXiExponencial.Rows(filaXiCuadrado).Cells(2).Value = fe
                dtgXiExponencial.Rows(filaXiCuadrado).Cells(3).Value = Math.Round(xiCalculado, 4)
                acumuladorFila = 0
                fo = 0
                fe = 0
                filaXiCuadrado += 1
            End If

            fila += 1

            vectorUltimaFila(3) = Math.Round(i + intervalo, 4) ''mantengo siempre el ultimo rango
        Next

        ''si habia mas intervalos por agrupar recalculo
        If acumuladorFila > 0 Then

            dtgXiExponencial.Rows(filaXiCuadrado - 1).Cells(0).Value = vectorUltimaFila(0).ToString + " - " + vectorUltimaFila(3).ToString
            fo += vectorUltimaFila(1)
            fe += vectorUltimaFila(2)
            dtgXiExponencial.Rows(filaXiCuadrado - 1).Cells(1).Value = fo
            dtgXiExponencial.Rows(filaXiCuadrado - 1).Cells(2).Value = fe
            xiCalculado = vectorUltimaFila(4) + (((fo - fe) ^ 2) / fe)
            dtgXiExponencial.Rows(filaXiCuadrado - 1).Cells(3).Value = Math.Round((((fo - fe) ^ 2) / fe), 4)

        End If
        ''termino de calcular los datos de xiCuadrado
        dtgXiExponencial.Rows.Add()
        lblVNormal.Text = ("los grados de libertad son: " + (intervalos - 2).ToString).ToString
        Dim xiTabulado As String = listaChipcuadrada(intervalos - 3)

        dtgXiExponencial.Rows(filaXiCuadrado).Cells(2).Value = "Xi Tabulado"
        If xiTabulado Is "" Then ''si la posicion del vector no tiene valores, cambia el string xiTabulado
            dtgXiExponencial.Rows(filaXiCuadrado).Cells(3).Value = "no hay valores"
        Else
            dtgXiExponencial.Rows(filaXiCuadrado).Cells(3).Value = xiTabulado.ToString
            dtgXiExponencial.Rows.Add()
            filaXiCuadrado += 1
            dtgXiExponencial.Rows((filaXiCuadrado)).Cells(2).Value = "Xi Calculado"
            dtgXiExponencial.Rows((filaXiCuadrado)).Cells(3).Value = xiCalculado.ToString
            dtgXiExponencial.Rows.Add()
            filaXiCuadrado += 1
            If Convert.ToDouble(xiTabulado.ToString) > xiCalculado Then
                dtgXiExponencial.Rows((filaXiCuadrado)).Cells(3).Value = "Aceptado"
                dtgXiExponencial.Rows((filaXiCuadrado)).Cells(3).Style.BackColor = Color.Green
            Else
                dtgXiExponencial.Rows((filaXiCuadrado)).Cells(3).Value = "Rechazado"
                dtgXiExponencial.Rows((filaXiCuadrado)).Cells(3).Style.BackColor = Color.Red
            End If
        End If
        chartExponencial.Series("Observado").ChartType = SeriesChartType.Column
        chartExponencial.Series("Observado").XValueType = ChartValueType.Int32
        chartExponencial.Series("Observado").YValueType = ChartValueType.Int32
        chartExponencial.ChartAreas(0).AxisX.Interval = 0.5
        chartExponencial.Series("Observado").IsValueShownAsLabel = True

    End Sub
End Class
