'Changed from 40 picture boxes
Const intMAXNUMBEROFBLOCKS As Integer = 30
Dim blnBallMoveRight As Boolean
Dim blnBallMoveUp As Boolean
Dim blnBarMoveRight As Boolean
Dim blnBarMoveLeft As Boolean
Dim picBlocks(intMAXNUMBEROFBLOCKS - 1) As PictureBox
Dim intScore As Integer = 0

Private Sub Form2_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    Call LoadBlocks(picBlocks)

End Sub

Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick

    Call MoveBall(blnBallMoveRight, blnBallMoveUp, Me.picBall, intScore)
    Call MoveBar(blnBarMoveRight, blnBarMoveLeft, Me.picBar)
    Me.lblScore.Text = intScore

End Sub

Private Sub Form2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

    If e.KeyValue = Keys.Right Then
        blnBarMoveRight = True
        blnBarMoveLeft = False
    ElseIf e.KeyValue = Keys.Left Then
        blnBarMoveLeft = True
        blnBarMoveRight = False
    End If

End Sub

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
'loads blocks
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Sub LoadBlocks(ByRef picBricks() As PictureBox)

    Dim intLoop As Integer
    Dim intLeftOne As Integer = 0
    Dim intLeftTwo As Integer
    Dim intLeftThree As Integer

    'Change pictures of each one

    For intLoop = 0 To intMAXNUMBEROFBLOCKS - 1

        If intLoop <= 9 Then
            intLeftOne = intLeftOne + 100
            picBricks(intLoop) = New PictureBox
            picBricks(intLoop).Location = New Point(1, 30)
            picBricks(intLoop).Left = intLeftOne
        ElseIf intLoop <= 19 Then
            intLeftTwo = intLeftTwo + 100
            picBricks(intLoop) = New PictureBox
            picBricks(intLoop).Location = New Point(1, 85)
            picBricks(intLoop).Left = intLeftTwo
        ElseIf intLoop <= 29 Then
            intLeftThree = intLeftThree + 100
            picBricks(intLoop) = New PictureBox
            picBricks(intLoop).Location = New Point(1, 140)
            picBricks(intLoop).Left = intLeftThree
        End If
        picBricks(intLoop).Width = 100
        picBricks(intLoop).Height = 50
        picBricks(intLoop).SizeMode = PictureBoxSizeMode.StretchImage
        picBricks(intLoop).Image = My.Resources.greensquare
        Controls.Add(picBricks(intLoop))

    Next intLoop

End Sub
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
'Moves ball when timer ticks.
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Sub MoveBall(ByRef blnRight As Integer, ByRef blnUp As Integer, ByRef picMovingBall As PictureBox, ByRef intCount As Integer)

    If blnRight = True Then
        picMovingBall.Left = picMovingBall.Left + 20
    Else
        picMovingBall.Left = picMovingBall.Left - 20
    End If

    If blnUp = True Then
        picMovingBall.Top = picMovingBall.Top - 20
    Else
        picMovingBall.Top = picMovingBall.Top + 20
    End If

    If picMovingBall.Left <= Me.ClientRectangle.Left Then
        blnRight = True
    End If

    If picMovingBall.Left + picMovingBall.Width >= Me.ClientRectangle.Right Then
        blnRight = False
    End If

    'Added 30 so it doesn't hit the menustrip at the top
    If picMovingBall.Top <= Me.ClientRectangle.Top + 30 Then
        blnUp = False
    End If

    If picMovingBall.Top + picMovingBall.Height >= Me.ClientRectangle.Bottom Then
        Timer1.Enabled = False
        'Change so it shows yes no
        MessageBox.Show("Oh no! The ball fell! Do you wish to try again?")

        If TouchedBar(picBall, picBar) = True Then
            'Call MoveBlocks()
            blnUp = True
        End If
    End If
End Sub

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
'Moves Bar
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Sub MoveBar(ByRef blnBarRight As Boolean, ByVal blnBarLeft As Boolean, ByVal picBottomBar As PictureBox)

    If blnBarRight = True Then
        picBottomBar.Left = picBottomBar.Left + 20
    ElseIf blnBarLeft = True Then
        picBottomBar.Left = picBottomBar.Left - 20
    End If

    If picBottomBar.Left <= Me.ClientRectangle.Left Then
        picBottomBar.Left = 0
    End If

    If picBottomBar.Left + picBottomBar.Width >= Me.ClientRectangle.Right Then
        picBottomBar.Left = 0
    End If

End Sub

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
'Moves blocks down when the ball touches the bar. 
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
'Sub MoveBlocks(ByVal picBar AS PictureBox, ByVal picBall As PictureBox, ByRef picBlocks() As Picturebox)

'If TouchedBar(picBall, picBar) = True Then


'End Sub
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
'Checks if the ball has touched the bar and returns true or false. 
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Function TouchedBar(ByVal picBall As PictureBox, ByVal picBar As PictureBox) As Boolean

    If picBall.Top + picBall.Height >= picBar.Top Then
        Return True
    End If
End Function

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
'Makes blocks invisible when they have been hit by the ball
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
'Sub HideBlocks(ByRef picBlocks() As PictureBox, ByRef picBall As PictureBox, ByRef intScore As Integer)

'    Dim picBlockBroken As PictureBox

'    picBlockBroken = BlockHit(picBlocks, picBall)
'    picBlockBroken.Visible = False
'    intScore = intScore + 50

'End Sub
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
'Checks if the ball has hit the block and returns true or false.
'
'***was supposed to be a boolean to check if the block was hit but changed to
'a picture box so the block that is actually hit can be found
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
'    Function BlockHit(ByVal picBlock() As PictureBox, ByVal picBall As PictureBox) As PictureBox

'        Dim intLoop As Integer

'        For intLoop = 0 To intMAXNUMBEROFBLOCKS
'            If (picBall.Top + picBall.Height >= picBlock(intLoop).Top) And _
'(picBall.Top <= picBlock(intLoop).Top + picBlock(intLoop).Height) And _
'(picBall.Left + picBall.Width >= picBlock(intLoop).Left) And (picBall.Left <= picBlock(intLoop).Left + picBlock(intLoop).Width) Then
'                Return picBlock(intLoop)
'            End If
'            Return picBlock(intLoop)
'        Next intLoop
'End Function

Private Sub mnuNewGame_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuNewGame.Click

End Sub

Private Sub mnuInstructions_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuInstructions.Click

    MessageBox.Show("Welcome to Brick Breaker! Hit the bricks by bouncing the ball on the bar at the bottom" & _
    " of the screen. Move the bar with the right and left arrow keys. Be careful, if the ball touches the " & _
    "bottom, the game will be over! Whenever the ball hits the bar, the bricks will move down too! Break all" & _
    " of the bricks before they touch the ground or the game will be over!")

End Sub

Private Sub mnuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuExit.Click

    End

End Sub

Private Sub mnuCredits_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuCredits.Click

    'microsoft
    MessageBox.Show("Thanks to SchoolFreeware's tutorials for help with timers, making objects move, " & _
                    "using multiple forms, and collision detection.")

End Sub
