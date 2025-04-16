#include <dnne.h>
#include <stdlib.h>
#include <stdio.h>

// Define override of built-in rude-abort API.
DNNE_API void dnne_abort(enum failure_type type, int error_code)
{
    printf("Custom dnne_abort() - %d, %#08x\n", type, error_code);
    exit(error_code);
}