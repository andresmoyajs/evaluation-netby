import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogFooter,
  DialogTitle,
  DialogDescription,
} from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Loader2 } from "lucide-react";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import type { Product } from "@/types/product";
import { toast } from "sonner";
import { changeStatusProduct } from "@/actions/productsAction";

export default function ProductStatusModal({
  product,
  setOpen,
  open,
}: {
  product: Product;
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
  open: boolean;
}) {
  const queryClient = useQueryClient();
  const mutation = useMutation({
    mutationFn: () => changeStatusProduct(product.id),
    onSuccess: () => {
      setOpen(false);
      toast.success(
        product.statusLabel === "ACTIVO"
          ? "El producto se ha desactivado exitosamente"
          : "El producto se ha activado exitosamente",
        { id: "status-product" }
      );

      queryClient.invalidateQueries({
        queryKey: ["products"],
      });
    },
    onError: () => {
      setOpen(false);
      toast.error(
        product.statusLabel === "ACTIVO"
          ? "El producto no se ha eliminado, inténtelo nuevamente"
          : "El producto no se ha activado, inténtelo nuevamente",
        { id: "status-product" }
      );
    },
  });

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Desactivar producto</DialogTitle>
          <DialogDescription>
            {product.statusLabel === "ACTIVO" ? (
              <>
                ¿Estás seguro de desactivar <strong>{product.name}</strong>?
              </>
            ) : (
              <>
                ¿Estás seguro de activar <strong>{product.name}</strong>?
              </>
            )}
          </DialogDescription>
        </DialogHeader>

        <DialogFooter className="flex justify-end gap-2">
          <Button
            variant="secondary"
            onClick={() => setOpen(false)}
            disabled={mutation.isPending}
          >
            Cancelar
          </Button>
          <Button
            variant={
              product.statusLabel === "ACTIVO" ? "destructive" : "default"
            }
            onClick={() => mutation.mutate()}
            disabled={mutation.isPending}
          >
            {mutation.isPending && (
              <Loader2 className="h-4 w-4 animate-spin mr-2" />
            )}
            Aceptar
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
}
