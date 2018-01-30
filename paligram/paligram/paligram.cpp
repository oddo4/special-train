// paligram.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <list>
#include <string>
#include <stdio.h>
#include <algorithm>

int main()
{
	std::cout << "Enter string: ";
	std::string input;
	std::getline(std::cin, input);
	std::string s;
	for (int i = 0; i < input.length(); i++) 
	{
		if (input[i] != ' ' && isalpha(input[i])) 
		{
			s += tolower(input[i]);
		}
	}
	std::string r(s.rbegin(),s.rend());
	std::cout << "input string: " << s << std::endl;
	std::cout << "output string: " << r << std::endl;
	if (s != "") 
	{
		if (s == r)
		{
			std::cout << input << " je palindrom." << std::endl;
		}
		else
		{
			std::cout << input << " neni palindrom." << std::endl;
		}
	}
	else
	{
		std::cout << "Spatny vstup!" << std::endl;
	}
    return 0;
}

