hmLoadTopic({
hmKeywords:"",
hmTitle:"ReleaseReference - пример использования",
hmDescription:"reference p = LineSeg (100, 50, 200, 50, 1); LightObj (p, 1); \/освобождается указатель на объект, \/после вызова ReleaseReference к объекту р обращаться нельзя ReleaseReferenc",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">ReleaseReference - пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1725183\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference p = LineSeg (100, 50, 200, 50, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LightObj (p, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/освобождается указатель на объект,<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/после вызова ReleaseReference к объекту р обращаться нельзя<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">ReleaseReference (p);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LightObj (p, 0);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/взводится ошибка &quot;В текущем документе объект не найден&quot;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">MessageBoxResult();<\/span><\/p>\n\r"
})
