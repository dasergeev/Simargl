hmLoadTopic({
hmKeywords:"",
hmTitle:"IsEnableTaskAccess - пример использования",
hmDescription:"\/\/проверяем доступ к задаче bool flagEnableTaskAccess = IsEnableTaskAccess(); Message (flagEnableTaskAccess ? \"Доступ разрешен\" : \"Доступ запрещен\"); if (flagEnableTaskAccess)",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">IsEnableTaskAccess - пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1729890\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/проверяем доступ к задаче<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">bool flagEnableTaskAccess = IsEnableTaskAccess();<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Message (flagEnableTaskAccess ? &quot;Доступ разрешен&quot; : &quot;Доступ запрещен&quot;);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (flagEnableTaskAccess)<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">{<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/запрещаем доступ<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">EnableTaskAccess (0);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/проверяем доступ к задаче<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">flagEnableTaskAccess = IsEnableTaskAccess();<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Message (flagEnableTaskAccess ? &quot;Доступ разрешен&quot; : &quot;Доступ запрещен&quot;);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">EnableTaskAccess (1); \/\/ разрешить доступ<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r"
})
