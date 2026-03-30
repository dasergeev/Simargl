hmLoadTopic({
hmKeywords:"",
hmTitle:"ksUserParam::number - пример использования",
hmDescription:"Получить номер команды пользовательской библиотеки, при помощи которой",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">ksUserParam::number - пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcq1950837\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Получить номер команды пользовательской библиотеки, при помощи которой<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">можно редактировать компонент.<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/получить интерфейс ksUserParam у KompasObject<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">ksUserParam userParam( kompasObject.GetParamStruct(ko_UserParam) );<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">userParam.Init();<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/заполнить параметры интерфейса<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">ksPart.GetUserParam( userParam );<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/Получить номер команды<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">long number = userParam.number;<\/span><\/p>\n\r"
})
