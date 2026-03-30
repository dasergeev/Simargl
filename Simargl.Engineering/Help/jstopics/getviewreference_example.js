hmLoadTopic({
hmKeywords:"",
hmTitle:"GetViewReference пример использования",
hmDescription:"\/\/получим указатель на первый вид reference v = GetViewReference (1); if (v) { ViewParam par; \/\/возьмем параметры вида if (GetObjParam (v, &par, sizeof (ViewParam), ALLPARA",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">GetViewReference пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1722756\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/получим указатель на первый вид<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference v = GetViewReference (1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (v)<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">{<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">ViewParam par;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/возьмем параметры вида<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (GetObjParam (v, &amp;par, sizeof (ViewParam), ALLPARAM))<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">{<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">char buf[255];<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">sprintf (buf,<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&quot;Параметры вида\\nx = %0.1f, y = %0.1f\\nscale=<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">%0.1f ang =%0.1f state=%d\\nname=%s&quot;,<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">par.x, par.y, par.scale, par.ang, par.state, par.name);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Message(buf);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r"
})
