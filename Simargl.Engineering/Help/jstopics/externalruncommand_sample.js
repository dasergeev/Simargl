hmLoadTopic({
hmKeywords:"",
hmTitle:"Пример использования функции ExternalRunCommand",
hmDescription:"Public Sub ExternalRunCommand(ByVal command As Integer, ByVal mode As Integer, ByVal Kompas As Object)",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">Пример использования функции ExternalRunCommand <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcq1924286\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Public Sub ExternalRunCommand(ByVal command As Integer, ByVal mode As Integer, ByVal Kompas As Object)<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Select Case command<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Case 1<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Dim document As Object \'КОМПАС-документ<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Set document = Kompas.Document3D \'3D-документ<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">If Not document is Nothing Then<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Kompas.ksMessage &quot;Получен 3D-документ&quot;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">End If<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Set document = Nothing \'освободить документ<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Case Else<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">MsgBox &quot;Команда &quot; + Str(command) + &quot; не выполнена&quot;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">End Select<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">End Sub<\/span><\/p>\n\r"
})
