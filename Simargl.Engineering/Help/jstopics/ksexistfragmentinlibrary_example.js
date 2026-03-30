hmLoadTopic({
hmKeywords:"",
hmTitle:"ksExistFragmentInLibrary - Пример использования",
hmDescription:"char frwName [250]; strcpy (frwName, \"C:\\\\0\\\\Детали.lfr|Фланцы|Исполнение 1\"); if (ReadString (\"Введите имя фрагмента или папки\", \/\/ строка приглашения frwName, 250)) {   in",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">ksExistFragmentInLibrary - Пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1719156\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">char frwName [250];<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">strcpy (frwName, &quot;C:\\\\0\\\\Детали.lfr|Фланцы|Исполнение 1&quot;);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (ReadString (&quot;Введите имя фрагмента или папки&quot;, \/\/ строка приглашения<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">frwName, 250)) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">int j = ksExistFragmentInLibrary(frwName);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">char buf [250];<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">sprintf (buf, &quot;%s\\n%s&quot;,frwName, j==0 ? &quot;нет фрагмента или папки&quot; : j==-1 ? &quot;нет библиотеки&quot; : &quot;фрагмент или папка есть&quot;);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Message(buf);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r"
})
