
Imports System.IO
Imports QRCoder
Imports ZXing

Public Class MMenu
    Public pos As Integer
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

        Add_User.Show()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Add_Item.Show()

    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Stock_Control.Show()
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        report_manager.Show()
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Stor_info.Show()
    End Sub

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        Operation.Show()
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        Promotions.Show()
    End Sub

    Private Sub MMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub MMenu_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If pos = 8 Then




            If e.KeyCode = Keys.F2 Then

                Add_Item.Show()


            End If

            If e.KeyCode = Keys.F3 Then
                Stock_Control.Show()



            End If
            If e.KeyCode = Keys.F4 Then

                Stor_info.Show()

            End If
            If e.KeyCode = Keys.F5 Then

                Promotions.Show()

            End If
            If e.KeyCode = Keys.F6 Then
                Operation.Show()


            End If
            If e.KeyCode = Keys.F1 Then

                Add_User.Show()

            End If

        End If
        If e.KeyCode = Keys.F7 Then

            report_manager.Show()


        End If

        If e.KeyCode = Keys.F8 Then




        End If
    End Sub

    Private Sub PictureBox1_MouseHover(sender As Object, e As EventArgs) Handles PictureBox1.MouseHover
        PictureBox1.Size = New Point(150, 150)
        PictureBox1.BackColor = Color.LightSkyBlue
    End Sub

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave
        PictureBox1.Size = New Point(100, 103)
        PictureBox1.BackColor = Color.Transparent
    End Sub

    Private Sub PictureBox2_MouseHover(sender As Object, e As EventArgs) Handles PictureBox2.MouseHover
        PictureBox2.Size = New Point(150, 150)
        PictureBox2.BackColor = Color.LightSkyBlue
    End Sub

    Private Sub PictureBox2_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox2.MouseLeave
        PictureBox2.Size = New Point(100, 103)
        PictureBox2.BackColor = Color.Transparent
    End Sub

    Private Sub PictureBox3_MouseHover(sender As Object, e As EventArgs) Handles PictureBox3.MouseHover
        PictureBox3.Size = New Point(150, 150)
        PictureBox3.BackColor = Color.LightSkyBlue
    End Sub

    Private Sub PictureBox3_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox3.MouseLeave
        PictureBox3.Size = New Point(100, 103)
        PictureBox3.BackColor = Color.Transparent
    End Sub

    Private Sub PictureBox4_MouseHover(sender As Object, e As EventArgs) Handles PictureBox4.MouseHover
        PictureBox4.Size = New Point(150, 150)
        PictureBox4.BackColor = Color.LightSkyBlue
    End Sub

    Private Sub PictureBox4_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox4.MouseLeave
        PictureBox4.Size = New Point(100, 103)
        PictureBox4.BackColor = Color.Transparent
    End Sub

    Private Sub PictureBox5_MouseHover(sender As Object, e As EventArgs) Handles PictureBox5.MouseHover
        PictureBox5.Size = New Point(150, 150)
        PictureBox5.BackColor = Color.LightSkyBlue
    End Sub

    Private Sub PictureBox5_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox5.MouseLeave
        PictureBox5.Size = New Point(100, 103)
        PictureBox5.BackColor = Color.Transparent
    End Sub

    Private Sub PictureBox6_MouseHover(sender As Object, e As EventArgs) Handles PictureBox6.MouseHover
        PictureBox6.Size = New Point(150, 150)
        PictureBox6.BackColor = Color.LightSkyBlue
    End Sub

    Private Sub PictureBox6_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox6.MouseLeave
        PictureBox6.Size = New Point(100, 103)
        PictureBox6.BackColor = Color.Transparent
    End Sub

    Private Sub PictureBox7_MouseHover(sender As Object, e As EventArgs) Handles PictureBox7.MouseHover
        PictureBox7.Size = New Point(150, 150)
        PictureBox7.BackColor = Color.LightSkyBlue
    End Sub

    Private Sub PictureBox7_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox7.MouseLeave
        PictureBox7.Size = New Point(100, 103)
        PictureBox7.BackColor = Color.Transparent
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage

    End Sub

    Private Sub MMenu_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing


    End Sub
End Class