hmLoadTopic({
hmKeywords:"",
hmTitle:"ksCouplingCircleCircle - пример использования",
hmDescription:"\/\/поиск окружностей сопряжения для двух окружностей",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">ksCouplingCircleCircle - пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1718163\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/поиск окружностей сопряжения для двух окружностей<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">double rad = 20;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">int kp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">CON con[8];<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Circle(100, 100, 100, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Circle(100, 150, 100, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">ksCouplingCircleCircle(100, 100, 100, \/\/параметры первой окружности<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">100, 150, 100, \/\/параметры второй окружности<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">rad, &amp;kp, con);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/отрисуем окружности и точки сопряжения<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">for (short i = 0 ; i&lt;8; i) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Circle(con[i].xc, con[i].yc, rad,2);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Point(con[i].x1,con[i].y1,i);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Point(con[i].x2,con[i].y2,i);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r"
})
