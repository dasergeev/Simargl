hmLoadTopic({
hmKeywords:"",
hmTitle:"ksGetQualityDefects - Пример использования",
hmDescription:"double high = 0, low = 0; \/\/ отклонения   char name[20]; \/\/ поле допуска ::strcpy(name, \"H7\"); \/\/ умолчательное значение if (ReadString(\"Укажите поле допуска\", name, 4)) { \/\/",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">ksGetQualityDefects - Пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1723289\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">double high = 0, low = 0; \/\/ отклонения<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">char name[20]; \/\/ поле допуска<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::strcpy(name, &quot;H7&quot;); \/\/ умолчательное значение<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (ReadString(&quot;Укажите поле допуска&quot;, name, 4)) { \/\/ запрос поля допуска у пользователя<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">double dimValue = 15; \/\/ значение размера в мм<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (ReadDouble(&quot;Введите размер в мм:&quot;, 15, 0, 500, &amp;dimValue)) { \/\/ запрос значения размера у пользователя<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::ksGetQualityDefects(name, dimValue, &amp;high, &amp;low, true\/*inMM*\/); \/\/ получить отклонения в мм<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">char buf[255]; \/\/ строка сообщения<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::sprintf(buf, &quot;Поле допуска %s\\nЗначение = %g, high = %g, low = %g&quot;,<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">name, dimValue, high, low); \/\/ формируем сообщение<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::Message(buf); \/\/ выводим сообщение<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r"
})
