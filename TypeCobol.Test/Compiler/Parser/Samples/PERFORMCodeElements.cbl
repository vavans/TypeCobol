000010 PERFORM MyProcedure1 THROUGH MyProcedure2
000020 10 TIMES
000010 PERFORM MyProcedure3
000020 VARYING MyIndex FROM 1 BY 2 UNTIL MyIndex < IterationCount
000010 PERFORM 8 TIMES
000020 DISPLAY "Hello"
000030 END-PERFORM
000010 PERFORM VARYING MyIndex FROM 1 BY 2 UNTIL MyIndex < 10
000020 DISPLAY "Hello"
000030 END-PERFORM