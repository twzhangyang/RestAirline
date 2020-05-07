output "registry_id" {
  value       = aws_ecr_repository.restairline.registry_id
  description = "Registry ID"
}

output "registry_url" {
  value       = aws_ecr_repository.restairline.repository_url
  description = "Repository URL"
}

output "repository_name" {
  value       = aws_ecr_repository.restairline.name
  description = "Registry name"
}