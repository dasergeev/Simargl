hmLoadTopic({
hmKeywords:"",
hmTitle:"ksCreateViewObject, ksEditViewObject - Пример использования",
hmDescription:"\/\/ создать объект в интерактивном режиме (позиционная линия выноски)",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">ksCreateViewObject, ksEditViewObject - Пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1720544\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/ создать объект в интерактивном режиме (позиционная линия выноски)<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference posLeader = ksCreateViewObject(POSLEADER_OBJ );<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (posLeader){<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LightObj(posLeader, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Message(&quot;Позиционная линия выноски&quot;);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LightObj(posLeader, 0);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (ksEditViewObject(posLeader)) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LightObj(posLeader, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Message(&quot;Отредактированная позиционная линия выноски&quot;);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LightObj(posLeader, 0);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r"
})
