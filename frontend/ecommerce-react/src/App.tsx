import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { ThemeProvider } from "./components/ui/theme-provider";
import Footer from "./components/layout/Footer";
import Header from "./components/layout/Header";
import { Navigate, Route, Routes } from "react-router-dom";
import Home from "./pages/Home";
import { Toaster } from "sonner";
import { useState } from "react";
import Products from "./pages/Products";

function App() {
  const [queryClient] = useState(() => new QueryClient());

  return (
    <QueryClientProvider client={queryClient}>
      <ThemeProvider defaultTheme="dark" storageKey="vite-ui-theme">
        <div className="flex flex-col min-h-screen">
          <Header />

          <main className="flex-1 p-6">
            <Routes>
              <Route path="/" element={<Home />} />
              <Route
                path="/productos"
                element={<Navigate to="/productos/1" />}
              />
              <Route path="/productos/:currentPage" element={<Products />} />
            </Routes>
          </main>

          <Footer />

          <Toaster />
        </div>
      </ThemeProvider>
    </QueryClientProvider>
  );
}

export default App;
