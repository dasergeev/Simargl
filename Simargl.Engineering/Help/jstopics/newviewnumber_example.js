hmLoadTopic({
hmKeywords:"",
hmTitle:"NewViewNumber пример использования",
hmDescription:"reference v; char buf[128]; int number = NewViewNumber(); ViewParam par; memset (&par, 0, sizeof (ViewParam)); par.x = 10; par.y = 20; par.scale = 0.5; par.ang = 45; par",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">NewViewNumber пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1722706\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference v;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">char buf[128];<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">int number = NewViewNumber();<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">ViewParam par;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">memset (&amp;par, 0, sizeof (ViewParam));<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">par.x = 10;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">par.y = 20;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">par.scale = 0.5;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">par.ang = 45;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">par.color = RGB(10,20,10);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">par.state = stACTIVE;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">strcpy (par.name, &quot;user view&quot;);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/создали вид<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">v=CreateSheetView(&amp;par, &amp;number);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">int numb = GetViewNumber( v);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">sprintf (buf,&quot;создали вид numb=%d, number=%d&quot;, numb, number);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Message(buf);<\/span><\/p>\n\r"
})
