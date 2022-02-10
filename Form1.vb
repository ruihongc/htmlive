Public Class Window1
    Dim theFileName As String = "about:blank"

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        Dim codeFile As String = Nothing

        OpenFileDialog1.InitialDirectory = "c:\"
        OpenFileDialog1.Filter = "HTML files (*.txt)|*.html|All files (*.*)|*.*"
        OpenFileDialog1.FilterIndex = 2
        OpenFileDialog1.RestoreDirectory = True

        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            theFileName = OpenFileDialog1.FileName
            ToolStripStatusLabel1.Text = theFileName

            Try
                codeFile = My.Computer.FileSystem.ReadAllText(theFileName)
                If (codeFile IsNot Nothing) Then
                    Text1.Text = codeFile
                End If
            Catch Ex As Exception
                MessageBox.Show("Error reading file. Error: " & Ex.Message)
            End Try
        End If

    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Close()
    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        Text1.Clear()
        theFileName = "about:blank"
        ToolStripStatusLabel1.Text = ""
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        If theFileName = "about:blank" Then
            SaveFileDialog1.Filter = "HTML Website|*.html|Text File|*.txt"
            SaveFileDialog1.Title = "Saving"
            SaveFileDialog1.ShowDialog()

            If SaveFileDialog1.FileName <> "" Then
                theFileName = SaveFileDialog1.FileName
                ToolStripStatusLabel1.Text = theFileName
                Dim fswrite As New System.IO.StreamWriter(theFileName)

                fswrite.Write(Text1.Text)
                fswrite.Close()
            End If
        Else
            Dim fswrite As New System.IO.StreamWriter(theFileName)

            fswrite.Write(Text1.Text)
            fswrite.Close()
        End If
    End Sub

    Private Sub Text1_TextChanged(sender As Object, e As EventArgs) Handles Text1.TextChanged
        WebBrowser1.DocumentText = Text1.Text
    End Sub

    Private Sub ReloadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReloadToolStripMenuItem.Click
        WebBrowser1.DocumentText = Text1.Text
    End Sub

    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        If Not String.IsNullOrEmpty(Text1.SelectedText) Then
            Clipboard.SetText(Text1.SelectedText)
        End If
    End Sub

    Private Sub CutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CutToolStripMenuItem.Click
        If Not String.IsNullOrEmpty(Text1.SelectedText) Then
            Clipboard.SetText(Text1.SelectedText)
            Text1.SelectedText = ""
        End If
    End Sub

    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        If Clipboard.GetDataObject().GetDataPresent(DataFormats.Text) Then
            Text1.SelectedText = Clipboard.GetText
        End If
    End Sub
End Class
