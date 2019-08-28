Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Data
Imports DevExpress.Xpf.Charts
Imports System.Collections.ObjectModel

Namespace WpfApplication45
	''' <summary>
	''' Interaction logic for MainWindow.xaml
	''' </summary>
	Partial Public Class MainWindow
		Inherits Window

		Public Sub New()
			InitializeComponent()
		End Sub
		Private chartDataField As ObservableCollection(Of DataObject)
		Public ReadOnly Property ChartData() As ObservableCollection(Of DataObject)
			Get
				If chartDataField Is Nothing Then
					chartDataField = CreateData(150)
				End If
				Return chartDataField
			End Get
		End Property


		Private Function CreateData(ByVal pointCount As Integer) As ObservableCollection(Of DataObject)
			Dim names() As String = { "Aaa", "Bbb", "Ccc", "Ddd", "Eee", "Fff", "Ggg" }
			Dim r As New Random()
			Dim collection As New ObservableCollection(Of DataObject)()
			Dim vX As Double = 50
			Dim vY As Double = 30
			For i As Integer = 0 To pointCount - 1
				vX = vX + r.Next(9) - 5
				vY = vY + r.Next(9) - 4
				collection.Add(New DataObject() With {
					.Name= names(r.Next(names.Length)),
					.Date = Date.Today.AddDays(i),
					.ValueX = vX,
					.ValueY = vY
				})
			Next i
			Return collection
		End Function

		Private Sub chartControl1_BoundDataChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Dim diagram As XYDiagram2D = TryCast(DirectCast(sender, ChartControl).Diagram, XYDiagram2D)
			Dim axisY As Axis2D = diagram.ActualAxisY
			axisY.VisualRange = New Range()
			Dim minValue As Double = diagram.Series.Select(Function(s) s.Points.Min(Function(p) p.Value)).Min() * 1.1
			Dim maxValue As Double = diagram.Series.Select(Function(s) s.Points.Max(Function(p) p.Value)).Max() * 1.1
			axisY.VisualRange.SetMinMaxValues(minValue, maxValue)




		End Sub
	End Class
	Public Class DataObject
		Public Property [Date]() As Date
		Public Property Name() As String
		Public Property ValueX() As Double
		Public Property ValueY() As Double
	End Class
End Namespace
