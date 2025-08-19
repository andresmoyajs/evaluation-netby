import { Link } from "react-router-dom";
import { ModeToggle } from "../ui/mode-toggle";
import { Avatar, AvatarFallback, AvatarImage } from "../ui/avatar";
import { Button } from "../ui/button";
import {
  NavigationMenu,
  NavigationMenuItem,
  NavigationMenuLink,
  NavigationMenuList,
} from "../ui/navigation-menu";

export default function Header() {
  return (
    <header className="flex items-center justify-between px-6 py-3 border-b bg-background">
      <div className="text-xl font-bold">Netby</div>

      <NavigationMenu>
        <NavigationMenuList>
          <NavigationMenuItem>
            <NavigationMenuLink asChild>
              <Link to="/" className="px-3 py-2">
                Inicio
              </Link>
            </NavigationMenuLink>
          </NavigationMenuItem>
          <NavigationMenuItem>
            <NavigationMenuLink asChild>
              <Link to="/productos/1" className="px-3 py-2">
                Productos
              </Link>
            </NavigationMenuLink>
          </NavigationMenuItem>
        </NavigationMenuList>
      </NavigationMenu>

      <div className="flex items-center gap-3">
        <ModeToggle />
        <Avatar>
          <AvatarImage src="https://github.com/shadcn.png" alt="@usuario" />
          <AvatarFallback>US</AvatarFallback>
        </Avatar>
      </div>
    </header>
  );
}
