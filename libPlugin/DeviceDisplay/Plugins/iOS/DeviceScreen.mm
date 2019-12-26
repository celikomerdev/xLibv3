#import <UIKit/UIKit.h>
extern "C"
{
	int _scaleFactor()
	{
		return [[UIScreen mainScreen] scale];
	}
}