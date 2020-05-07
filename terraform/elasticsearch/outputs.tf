output "public_dns" {
  description = "List of public DNS names assigned to the instances"
  value       = aws_instance.es.public_dns
}

output "vpc_security_group_ids" {
  description = "List of VPC security group ids assigned to the instances"
  value       = aws_instance.es.vpc_security_group_ids
}

output "tags" {
  description = "List of tags"
  value       = aws_instance.es.tags
}

output "placement_group" {
  description = "List of placement group"
  value       = aws_instance.es.placement_group
}

output "instance_id" {
  description = "EC2 instance ID"
  value       = aws_instance.es.id
}

output "instance_public_dns" {
  description = "Public DNS name assigned to the EC2 instance"
  value       = aws_instance.es.public_dns
}